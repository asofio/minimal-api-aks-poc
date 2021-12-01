param containerRegistryName string = 'sofiominimalapipoc'
param aksName string = 'sofiominimalaks'


resource containerRegistry 'Microsoft.ContainerRegistry/registries@2021-06-01-preview' = {
  name: containerRegistryName
  location: resourceGroup().location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

resource aksCluster 'Microsoft.ContainerService/managedClusters@2021-03-01' = {
  name: aksName
  location: resourceGroup().location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    kubernetesVersion: '1.21.2'
    dnsPrefix: '${aksName}-dns'
    enableRBAC: true
    agentPoolProfiles: [
      {
        name: 'agentpool'
        count: 3
        vmSize: 'Standard_A2_v2'
        mode: 'System'
      }
    ]
  }
}
 
@allowed([
  'b24988ac-6180-42a0-ab88-20f7382dd24c' // Contributor
  'acdd72a7-3385-48ef-bd42-f606fba81ae7' // Reader
])
param roleAcrPull string = 'b24988ac-6180-42a0-ab88-20f7382dd24c'
 
resource assignAcrPullToAks 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(resourceGroup().id, containerRegistryName, 'AssignAcrPullToAks')
  scope: containerRegistry
  properties: {
    description: 'Assign AcrPull role to AKS'
    principalId: aksCluster.properties.identityProfile.kubeletidentity.objectId
    principalType: 'ServicePrincipal'
    roleDefinitionId: '/subscriptions/${subscription().subscriptionId}/providers/Microsoft.Authorization/roleDefinitions/${roleAcrPull}'
  }
  dependsOn:[
    aksCluster
    containerRegistry
  ]
}
