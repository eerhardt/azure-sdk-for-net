// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;
using Azure.Core;
using Azure.ResourceManager.ExtendedLocations.Models;
using Azure.ResourceManager.Models;

[assembly: CodeGenSuppressType("CustomLocationData")]
namespace Azure.ResourceManager.ExtendedLocations
{
    /// <summary> A class representing the CustomLocation data model. </summary>
    public partial class CustomLocationData : TrackedResourceData
    {
        /// <summary> Initializes a new instance of CustomLocationData. </summary>
        /// <param name="location"> The location. </param>
        public CustomLocationData(AzureLocation location) : base(location)
        {
            ClusterExtensionIds = new ChangeTrackingList<ResourceIdentifier>();
        }

        /// <summary> Initializes a new instance of CustomLocationData. </summary>
        /// <param name="id"> The id. </param>
        /// <param name="name"> The name. </param>
        /// <param name="resourceType"> The resourceType. </param>
        /// <param name="systemData"> The systemData. </param>
        /// <param name="tags"> The tags. </param>
        /// <param name="location"> The location. </param>
        /// <param name="identity"> Identity for the resource. Current supported identity types: SystemAssigned, None.</param>
        /// <param name="authentication">
        /// This is optional input that contains the authentication that should be used to generate the namespace.
        /// </param>
        /// <param name="clusterExtensionIds">
        /// Contains the reference to the add-on that contains charts to deploy CRDs and operators.
        /// </param>
        /// <param name="displayName">
        /// Display name for the Custom Locations location.
        /// </param>
        /// <param name="hostResourceId">
        /// Connected Cluster or AKS Cluster. The Custom Locations RP will perform a checkAccess API for listAdminCredentials permissions.
        /// </param>
        /// <param name="hostType">
        /// Type of host the Custom Locations is referencing (Kubernetes, etc...).
        /// </param>
        /// <param name="namespace">
        /// Kubernetes namespace that will be created on the specified cluster.
        /// </param>
        /// <param name="provisioningState">
        /// Provisioning State for the Custom Location.
        /// </param>
        internal CustomLocationData(ResourceIdentifier id, string name, ResourceType resourceType, ResourceManager.Models.SystemData systemData, IDictionary<string, string> tags, AzureLocation location, ResourceManager.Models.ManagedServiceIdentity identity, CustomLocationAuthentication authentication, IList<ResourceIdentifier> clusterExtensionIds, string displayName, ResourceIdentifier hostResourceId, CustomLocationHostType? hostType, string @namespace, string provisioningState) : base(id, name, resourceType, systemData, tags, location)
        {
            Identity = identity;
            Authentication = authentication;
            ClusterExtensionIds = clusterExtensionIds;
            DisplayName = displayName;
            HostResourceId = hostResourceId;
            HostType = hostType;
            Namespace = @namespace;
            ProvisioningState = provisioningState;
        }

        /// <summary> Identity for the resource. Current supported identity types: SystemAssigned, None.</summary>
        public ResourceManager.Models.ManagedServiceIdentity Identity { get; set; }
        /// <summary>
        /// This is optional input that contains the authentication that should be used to generate the namespace.
        /// </summary>
        public CustomLocationAuthentication Authentication { get; set; }
        /// <summary>
        /// Contains the reference to the add-on that contains charts to deploy CRDs and operators.
        /// </summary>
        public IList<ResourceIdentifier> ClusterExtensionIds { get; }
        /// <summary>
        /// Display name for the Custom Locations location.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Connected Cluster or AKS Cluster. The Custom Locations RP will perform a checkAccess API for listAdminCredentials permissions.
        /// </summary>
        public ResourceIdentifier HostResourceId { get; set; }
        /// <summary>
        /// Type of host the Custom Locations is referencing (Kubernetes, etc...).
        /// </summary>
        public CustomLocationHostType? HostType { get; set; }
        /// <summary>
        /// Kubernetes namespace that will be created on the specified cluster.
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// Provisioning State for the Custom Location.
        /// </summary>
        public string ProvisioningState { get; set; }
    }
}
