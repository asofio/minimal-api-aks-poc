# Deploy to AKS

Now, lets take some steps to deploy this API to Azure Kubernetes Service.

### Setup Azure Infrastructure

First, create the necessary Azure infrastructure to be able to deploy the API to Azure.  The steps below will walk you through running the contained bicep script against an Azure resource group.

Within your Azure subscription, ensure that you have a resource group that you can deploy to.  Either create one manually through the portal or execute the following az cli command.

`az group create --name YourResourceGroupNameHere --location eastus`

NOTE: Ensure that you have logged in using `az login` and that you are pointed at your desired subscription using `az account list -o table`.  Use `az account set --subscription <name or id here>` to switch subscriptions.

Next, make two small adjustments to the parameters within the deployment.bicep file.  Within VSCode, open the /deployment/deployment.bicep file and fill in values for the two parameters you see.  This will be the resource name for the AKS and Azure Container Registry (ACR) resources that the bicep script will create.

Now, execute the bicep script by navigating to the `deployment` folder and running the following command.

`az deployment group create --resource-group YourResourceGroupNameHere --template-file deployment.bicep`

Take a quick break while the deployment runs.  Upon completion, navigate to your resource group in the azure portal and ensure that you see both an AKS and ACR resource.

### Containerize the API

The next step in deploying the API to Azure will be to containerize it.  Before performing this step, ensure that Docker is running.

Navigate to the root of the VSCode project and run the following command to containerize the API.  This command will build a docker image and tag it so that it can be pushed to our ACR instance.  In the command below, fill in the name of your ACR resource.

`docker build -t sofiocr.azurecr.io/minimalapipoc -f Dockerfile .`

Congratulations! You now have an image that is ready to be pushed to ACR. (Run `docker images` to see your newly created image.)

### Push Container to Azure Container Registry (ACR) 

Next, lets push our container image to ACR.

First, login to the ACR instance.

`az acr login --name YourACRResourceNameHere.azurecr.io `

Now, push the image to ACR

`docker push YourACRResourceNameHere.azurecr.io/minimalapipoc`

### Test the API in AKS

Now that we have a container pushed to ACR, we can instruct AKS to pull the image and expose a service so that we can communicate with the API.

Navigate to the `/deployment/aks` folder and run the following commands.

`az aks get-credentials -g YourResourceGroupNameHere -n YourAKSInstanceNameHere`

`kubectl apply -f deployment.yaml`

`kubectl apply -f service.yaml`

Use `kubectl get pods` to monitor the deployment of the image to AKS.  Also, execute `kubectl get svc` to look up the external IP that was assigned to the service that was just deployed.  You should see something like this:

![Kubernetes Service](/assets/service.png)

The IP address you see under EXTERNAL-IP (yours will differ from what is shown) is what we will use to ensure that our image has been pulled and is successfully running in AKS.  In a browser window, visit the following address: `http://[YourExternalIPHere]:8080/`

Congratulations!  You have now deployed a .NET 6 Minimal API to AKS!

## Next

Lets create the ability to re-use this API by [creating a dotnet new template](dotnet-new-template.md).
