using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.Models
{
    public class DocketCRMResponse
    {
        public Createdby createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object createdOnBehalfBy { get; set; }
        public object fsdyn_Aboutcommunityuserinvolvement { get; set; }
        public object fsdyn_AboutFinancialCapabitlity { get; set; }
        public object fsdyn_AboutGovernanceAndManagement { get; set; }
        public object fsdyn_AboutOrganisation { get; set; }
        public object fsdyn_AboutSafeguarding { get; set; }
        public object fsdyn_Aboutthepeoplemetattheassessment { get; set; }
        public object fsdyn_Access { get; set; }
        public object fsdyn_ClosingSection { get; set; }
        public object fsdyn_InternalReviewer { get; set; }
        public bool fsdyn_InvolvesCommunityorSectoritserves { get; set; }
        public bool fsdyn_IsAccessibleToCommunity { get; set; }
        public bool fsdyn_IsOrganisationFinanciallyViable { get; set; }
        public bool fsdyn_IsOrganisationWellRun { get; set; }
        public bool fsdyn_IsSafeguardingEmbeddedInOrg { get; set; }
        public object fsdyn_Recommendataion { get; set; }
        public object fsdyn_SummaryofAssessment { get; set; }
        public object importSequenceNumber { get; set; }
        public Modifiedby modifiedBy { get; set; }
        public DateTime modifiedOn { get; set; }
        public object modifiedOnBehalfBy { get; set; }
        public object msnfp_AwardId { get; set; }
        public object msnfp_AwardVersionId { get; set; }
        public object msnfp_DateSubmitted { get; set; }
        public object msnfp_Description { get; set; }
        public object msnfp_DueDate { get; set; }
        public string msnfp_Name { get; set; }
        public Msnfp_Requestid msnfp_RequestId { get; set; }
        public Msnfp_Review_Docketid msnfp_Review_DocketId { get; set; }
        public object msnfp_Review_LeadId { get; set; }
        public object msnfp_Review_ReportId { get; set; }
        public object msnfp_ReviewDate { get; set; }
        public object msnfp_ReviewerId { get; set; }
        public Msnfp_Reviewertype msnfp_ReviewerType { get; set; }
        public string msnfp_ReviewId { get; set; }
        public string id { get; set; }
        public object msnfp_Status { get; set; }
        public Msnfp_Type msnfp_Type { get; set; }
        public object overriddenCreatedOn { get; set; }
        public Ownerid ownerId { get; set; }
        public Owningbusinessunit owningBusinessUnit { get; set; }
        public object owningTeam { get; set; }
        public Owninguser owningUser { get; set; }
        public int stateCode { get; set; }
        public Statuscode statusCode { get; set; }
        public object timeZoneRuleVersionNumber { get; set; }
        public object utcConversionTimeZoneCode { get; set; }
        public object versionNumber { get; set; }
        public object msnfp_request_review_RequestId { get; set; }
        public object msnfp_review_docket { get; set; }
        public object msnfp_review_reviewer { get; set; }
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
        public Extensiondata10 extensionData { get; set; }
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

    public class Modifiedby
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

    public class Msnfp_Requestid
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

    public class Msnfp_Review_Docketid
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

    public class Msnfp_Reviewertype
    {
        public int value { get; set; }
        public Extensiondata4 extensionData { get; set; }
    }

    public class Extensiondata4
    {
    }

    public class Msnfp_Type
    {
        public int value { get; set; }
        public Extensiondata5 extensionData { get; set; }
    }

    public class Extensiondata5
    {
    }

    public class Ownerid
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

    public class Owningbusinessunit
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public object name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata7 extensionData { get; set; }
    }

    public class Extensiondata7
    {
    }

    public class Owninguser
    {
        public string id { get; set; }
        public string logicalName { get; set; }
        public object name { get; set; }
        public object[] keyAttributes { get; set; }
        public object rowVersion { get; set; }
        public Extensiondata8 extensionData { get; set; }
    }

    public class Extensiondata8
    {
    }

    public class Statuscode
    {
        public int value { get; set; }
        public Extensiondata9 extensionData { get; set; }
    }

    public class Extensiondata9
    {
    }

    public class Extensiondata10
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
