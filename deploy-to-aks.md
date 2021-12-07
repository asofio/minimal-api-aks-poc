# Deploy to AKS

Now, lets take some steps to deploy this API to Azure Kubernetes Service.

### Setup Azure Infrastructure

First, create the necessary Azure infrastructure to be able to deploy the API to Azure.  The steps below will walk you through running the contained bicep script against an Azure resource group.

Within your Azure subscription, ensure that you have a resource group that you can deploy to.  Either create one manually through the portal or execute the following az cli command:

```bash
# First set up some environment variables to reuse later
RESOURCE_GROUP=<YourResourceGroupNameHere>
LOCATION=eastus
ACR_NAME=<YourAcrName>
CLUSTER_NAME=<YourClusterName>

az group create --name $RESOURCE_GROUP --location $LOCATION
```


>*NOTE:* Ensure that you have logged in using `az login` and that you are pointed at your desired subscription using `az account list -o table`.  Use `az account set --subscription <name or id here>` to switch subscriptions.

Now, execute the bicep script by navigating to the `deployment` folder and running the following command:

```bash
az deployment group create \
--resource-group $RESOURCE_GROUP \
--template-file deployment.bicep \
--parameters containerRegistryName=$ACR_NAME \
aksName=$CLUSTER_NAME
```

### What is this creating?

This script will perform three main actions:

1. The Azure Container Registry (ACR) instance will be created.
2. A simple Azure Kubernetes Service (AKS) cluster will be created.
3. The managed identity of the AKS cluster is given authority to pull images from ACR.  This is incredibly important for an upcoming step in which we will apply a deployment to the AKS cluster.

Take a quick break while the deployment runs.  Upon completion, navigate to your resource group in the azure portal and ensure that you see both an AKS and ACR resource.

<br>

### Containerize the API

The next step in deploying the API to Azure will be to containerize it.  Before performing this step, ensure that Docker is running.

Navigate to the root of the VS Code project and run the following command to use Azure Container Registry to build your image.

```bash
az acr build --registry $ACR_NAME --image minimalapipoc --platform linux .
```

>*Note:* You could also build the image locally and push to ACR using the following commands <br>
```bash
docker build -t $ACR_NAME.azurecr.io/minimalapipoc -f Dockerfile .
az acr login --name $ACR_NAME.azurecr.io
docker push $ACR_NAME.azurecr.io/minimalapipoc
```

Congratulations! You now have an image that is ready to use in your ACR. (You can go out to the Azure Portal and have a look at your container registry to see the newly built image)

### Test the API in AKS

Now that we have a container pushed to ACR, we can instruct AKS to pull the image and expose a service so that we can communicate with the API.

First, run the following command to obtain the credentials for your AKS instance:

```bash
az aks get-credentials -g $RESOURCE_GROUP -n $CLUSTER_NAME
```

Navigate to the `/deployment/manifests` folder and do the following:

In the `deployment.yaml` file adjust line 19 and replace "[YourACRInstanceNameHere]" with the name of your ACR instance you deployed earlier.  After doing so, run the following commands:

```bash
# Create the deployment
kubectl apply -f deployment.yaml

# Expose your deployment as a service
kubectl apply -f service.yaml

# Watch the service and pods come online
watch kubectl get svc,pods

# Example Output
NAME                               TYPE           CLUSTER-IP    EXTERNAL-IP     PORT(S)          AGE
service/kubernetes                 ClusterIP      10.0.0.1      <none>          443/TCP          31m
service/minimalapiakspoc-service   LoadBalancer   10.0.84.126   52.150.42.173   8080:30757/TCP   98s

NAME                                               READY   STATUS    RESTARTS   AGE
pod/minimalapiakspoc-deployment-7cb8bc6b55-mqhcl   1/1     Running   0          104s
pod/minimalapiakspoc-deployment-7cb8bc6b55-p9nrr   1/1     Running   0          104s
```

Use `kubectl get pods` to monitor the deployment of the image to AKS.  Execute `kubectl get svc` to look up the external IP that was assigned to the service that was just deployed.  You should see something like this:

![Kubernetes Service](/assets/service.png)

The IP address you see under EXTERNAL-IP (yours will differ from what is shown) is what we will use to ensure that our image has been pulled and is successfully running in AKS.  In a browser window, visit the following address: `http://[YourExternalIPHere]:8080/`

Congratulations!  You have now deployed a .NET 6 Minimal API to AKS!

## Additional Resources

- [AKS Documentation](https://docs.microsoft.com/en-us/azure/aks/)
- [Kubernetes Service](https://kubernetes.io/docs/concepts/services-networking/service/)
- [Introduction to Bicep](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview)

## Next

Lets create the ability to re-use this API by [creating a dotnet new template](dotnet-new-template.md).
