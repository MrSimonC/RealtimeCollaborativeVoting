using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.Models.DocketCRMRequestResponse
{

    public class DocketCRMRequestResponse
    {
        public Createdby createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object createdOnBehalfBy { get; set; }
        public float exchangeRate { get; set; }
        public Fsdyn_Deliveryframework fsdyn_DeliveryFramework { get; set; }
        public string fsdyn_RecipientName { get; set; }
        public Fsdyn_Strengthingcommunitiesapp fsdyn_StrengthingCommunitiesApp { get; set; }
        public object importSequenceNumber { get; set; }
        public Modifiedby modifiedBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public object modifiedOnBehalfBy { get; set; }
        public object msnfp_AmountProjected { get; set; }
        public object msnfp_amountprojected_Base { get; set; }
        public object msnfp_AmountRecommended { get; set; }
        public object msnfp_amountrecommended_Base { get; set; }
        public Msnfp_Amountrequested msnfp_AmountRequested { get; set; }
        public Msnfp_Amountrequested_Base msnfp_amountrequested_Base { get; set; }
        public string msnfp_ApplicationID { get; set; }
        public object msnfp_ConflictofInterestDetail { get; set; }
        public object msnfp_ConflictofInterestStatus { get; set; }
        public object msnfp_DocketId { get; set; }
        public object msnfp_FiscalSponsorId { get; set; }
        public object msnfp_InitialApplicationChannel { get; set; }
        public object msnfp_LeadId { get; set; }
        public string msnfp_name { get; set; }
        public string? msnfp_Purpose { get; set; }
        public Msnfp_Recipientid msnfp_RecipientId { get; set; }
        public object msnfp_RenewalofAwardId { get; set; }
        public int? msnfp_RequestedDuration { get; set; }
        public DateTime? msnfp_RequestedStartDate { get; set; } // simon edit
        public object msnfp_requestId { get; set; } // simon edit
        public string id { get; set; }
        public object msnfp_RequestType { get; set; }
        public object msnfp_Requirements { get; set; }
        public Msnfp_Stage msnfp_Stage { get; set; }
        public Msnfp_Submittedbyid msnfp_SubmittedById { get; set; }
        public DateTime msnfp_SubmittedDate { get; set; }
        public object msnfp_TotalProjectBudget { get; set; }
        public object msnfp_totalprojectbudget_Base { get; set; }
        public object overriddenCreatedOn { get; set; }
        public Ownerid ownerId { get; set; }
        public Owningbusinessunit owningBusinessUnit { get; set; }
        public object owningTeam { get; set; }
        public Owninguser owningUser { get; set; }
        public object processid { get; set; }
        public object stageid { get; set; }
        public int stateCode { get; set; }
        public Statuscode statusCode { get; set; }
        public int? timeZoneRuleVersionNumber { get; set; }
        public Transactioncurrencyid transactionCurrencyId { get; set; }
        public object traversedpath { get; set; }
        public object utcConversionTimeZoneCode { get; set; }
        public object versionNumber { get; set; }
        public object msnfp_request_review_RequestId { get; set; }
        public object fsdyn_msnfp_deliveryframework_msnfp_request_DeliveryFramework { get; set; }
        public object msnfp_account_request_CustomerId { get; set; }
        public object msnfp_account_request_FiscalSponsor { get; set; }
        public object msnfp_contact_request_CustomerId { get; set; }
        public object msnfp_contact_request_SubmittedById { get; set; }
        public object msnfp_docket_request_DocketId { get; set; }
        public object msnfp_request_StrengthingCommunitiesApp_f { get; set; }
        public string logicalName { get; set; }
        public Attribute[] attributes { get; set; }
        public int entityState { get; set; }
        public Formattedvalue[] formattedValues { get; set; }
        public object[] relatedEntities { get; set; }
        public string rowVersion { get; set; }
        public object[] keyAttributes { get; set; }
        public bool hasLazyFileAttribute { get; set; }
        public object lazyFileAttributeKey { get; set; }
        public object lazyFileAttributeValue { get; set; }
        public object lazyFileSizeAttributeKey { get; set; }
        public int lazyFileSizeAttributeValue { get; set; }
        public Extensiondata14 extensionData { get; set; }
    }

    public class Createdby
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata extensionData { get; set; }
    }

    public class Extensiondata
    {
    }

    public class Fsdyn_Deliveryframework
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata1 extensionData { get; set; }
    }

    public class Extensiondata1
    {
    }

    public class Fsdyn_Strengthingcommunitiesapp
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata2 extensionData { get; set; }
    }

    public class Extensiondata2
    {
    }

    public class Modifiedby
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata3 extensionData { get; set; }
    }

    public class Extensiondata3
    {
    }

    public class Msnfp_Amountrequested
    {
        public float? value { get; set; }
        public Extensiondata4 extensionData { get; set; }
    }

    public class Extensiondata4
    {
    }

    public class Msnfp_Amountrequested_Base
    {
        public float value { get; set; }
        public Extensiondata5 extensionData { get; set; }
    }

    public class Extensiondata5
    {
    }

    public class Msnfp_Recipientid
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata6 extensionData { get; set; }
    }

    public class Extensiondata6
    {
    }

    public class Msnfp_Stage
    {
        public int value { get; set; }
        public Extensiondata7 extensionData { get; set; }
    }

    public class Extensiondata7
    {
    }

    public class Msnfp_Submittedbyid
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata8 extensionData { get; set; }
    }

    public class Extensiondata8
    {
    }

    public class Ownerid
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata9 extensionData { get; set; }
    }

    public class Extensiondata9
    {
    }

    public class Owningbusinessunit
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public object name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata10 extensionData { get; set; }
    }

    public class Extensiondata10
    {
    }

    public class Owninguser
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public object name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata11 extensionData { get; set; }
    }

    public class Extensiondata11
    {
    }

    public class Statuscode
    {
        public int value { get; set; }
        public Extensiondata12 extensionData { get; set; }
    }

    public class Extensiondata12
    {
    }

    public class Transactioncurrencyid
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public string name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata13 extensionData { get; set; }
    }

    public class Extensiondata13
    {
    }

    public class Extensiondata14
    {
    }

    public class Attribute
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }

    public class Formattedvalue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

}
