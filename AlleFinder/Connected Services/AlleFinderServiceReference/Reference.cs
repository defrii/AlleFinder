﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlleFinder.AlleFinderServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AlleFinderServiceReference.IAlleFinderService")]
    public interface IAlleFinderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetFiltersListByCategoryIdJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetFiltersListByCategoryIdJsonResponse")]
        string GetFiltersListByCategoryIdJson(string categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetFiltersListByCategoryIdJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetFiltersListByCategoryIdJsonResponse")]
        System.Threading.Tasks.Task<string> GetFiltersListByCategoryIdJsonAsync(string categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetItemsListJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetItemsListJsonResponse")]
        string GetItemsListJson(string filtersJson, int resultSize, int resultOffset);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetItemsListJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetItemsListJsonResponse")]
        System.Threading.Tasks.Task<string> GetItemsListJsonAsync(string filtersJson, int resultSize, int resultOffset);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListJsonResponse")]
        string GetCategoriesListJson();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListJsonResponse")]
        System.Threading.Tasks.Task<string> GetCategoriesListJsonAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListByPhraseJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListByPhraseJsonResponse")]
        string GetCategoriesListByPhraseJson(string phrase);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListByPhraseJson", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListByPhraseJsonResponse")]
        System.Threading.Tasks.Task<string> GetCategoriesListByPhraseJsonAsync(string phrase);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListPathsByPhrase", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListPathsByPhraseResponse")]
        string[] GetCategoriesListPathsByPhrase(string phrase);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlleFinderService/GetCategoriesListPathsByPhrase", ReplyAction="http://tempuri.org/IAlleFinderService/GetCategoriesListPathsByPhraseResponse")]
        System.Threading.Tasks.Task<string[]> GetCategoriesListPathsByPhraseAsync(string phrase);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAlleFinderServiceChannel : AlleFinder.AlleFinderServiceReference.IAlleFinderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AlleFinderServiceClient : System.ServiceModel.ClientBase<AlleFinder.AlleFinderServiceReference.IAlleFinderService>, AlleFinder.AlleFinderServiceReference.IAlleFinderService {
        
        public AlleFinderServiceClient() {
        }
        
        public AlleFinderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AlleFinderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AlleFinderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AlleFinderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetFiltersListByCategoryIdJson(string categoryId) {
            return base.Channel.GetFiltersListByCategoryIdJson(categoryId);
        }
        
        public System.Threading.Tasks.Task<string> GetFiltersListByCategoryIdJsonAsync(string categoryId) {
            return base.Channel.GetFiltersListByCategoryIdJsonAsync(categoryId);
        }
        
        public string GetItemsListJson(string filtersJson, int resultSize, int resultOffset) {
            return base.Channel.GetItemsListJson(filtersJson, resultSize, resultOffset);
        }
        
        public System.Threading.Tasks.Task<string> GetItemsListJsonAsync(string filtersJson, int resultSize, int resultOffset) {
            return base.Channel.GetItemsListJsonAsync(filtersJson, resultSize, resultOffset);
        }
        
        public string GetCategoriesListJson() {
            return base.Channel.GetCategoriesListJson();
        }
        
        public System.Threading.Tasks.Task<string> GetCategoriesListJsonAsync() {
            return base.Channel.GetCategoriesListJsonAsync();
        }
        
        public string GetCategoriesListByPhraseJson(string phrase) {
            return base.Channel.GetCategoriesListByPhraseJson(phrase);
        }
        
        public System.Threading.Tasks.Task<string> GetCategoriesListByPhraseJsonAsync(string phrase) {
            return base.Channel.GetCategoriesListByPhraseJsonAsync(phrase);
        }
        
        public string[] GetCategoriesListPathsByPhrase(string phrase) {
            return base.Channel.GetCategoriesListPathsByPhrase(phrase);
        }
        
        public System.Threading.Tasks.Task<string[]> GetCategoriesListPathsByPhraseAsync(string phrase) {
            return base.Channel.GetCategoriesListPathsByPhraseAsync(phrase);
        }
    }
}
