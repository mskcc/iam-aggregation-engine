using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;

/// <summary>
/// Service Now application entity.
/// </summary>
[Table("APM_ServiceNow_Applications_Info")]
public class ServiceNowApplicationUnusable
{
    /// <summary>
    /// Represents the primary key for the DTO (Data Transfer Object).
    /// </summary>
    [Key]
    public Guid PrimaryKey { get; set; }
    
    /// <summary>
    /// Gets or sets the attested date.
    /// </summary>
    public string? AttestedDate { get; set; }

    /// <summary>
    /// Gets or sets the application complexity.
    /// </summary>
    public string? ApplicationComplexity { get; set; }

    /// <summary>
    /// Gets or sets the operational status.
    /// </summary>
    public string? OperationalStatus { get; set; }

    /// <summary>
    /// Gets or sets the AI capability.
    /// </summary>
    public string? AiCapability { get; set; }

    /// <summary>
    /// Gets or sets the reasoning.
    /// </summary>
    public string? Reasoning { get; set; }

    /// <summary>
    /// Gets or sets the system updated on date.
    /// </summary>
    public string? SysUpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the department code.
    /// </summary>
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// Gets or sets the install type.
    /// </summary>
    public string? InstallType { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Gets or sets the discovery source.
    /// </summary>
    public string? DiscoverySource { get; set; }

    /// <summary>
    /// Gets or sets the first discovered date.
    /// </summary>
    public string? FirstDiscovered { get; set; }

    /// <summary>
    /// Gets or sets the due in date.
    /// </summary>
    public string? DueIn { get; set; }

    /// <summary>
    /// Gets or sets the MSK ADDM key.
    /// </summary>
    public string? MskAddmKey { get; set; }

    /// <summary>
    /// Gets or sets the IT application owner.
    /// </summary>
    public string? ItApplicationOwner { get; set; }

    /// <summary>
    /// Gets or sets the APM business process.
    /// </summary>
    public string? ApmBusinessProcess { get; set; }

    /// <summary>
    /// Gets or sets the GL account.
    /// </summary>
    public string? GlAccount { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Gets or sets the system created by.
    /// </summary>
    public string? SysCreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the warranty expiration date.
    /// </summary>
    public string? WarrantyExpiration { get; set; }

    /// <summary>
    /// Gets or sets the organization unit count.
    /// </summary>
    public string? OrganizationUnitCount { get; set; }

    /// <summary>
    /// Gets or sets the annual cost.
    /// </summary>
    public string? AnnualCost { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application is active.
    /// </summary>
    public string? Active { get; set; }

    /// <summary>
    /// Gets or sets the owned by link value.
    /// </summary>
    public string? OwnedBy { get; set; }

    /// <summary>
    /// Gets or sets the checked out status.
    /// </summary>
    public string? CheckedOut { get; set; }

    /// <summary>
    /// Gets or sets the report year.
    /// </summary>
    public string? RptYear { get; set; }

    /// <summary>
    /// Gets or sets the system domain path.
    /// </summary>
    public string? SysDomainPath { get; set; }

    /// <summary>
    /// Gets or sets the MSK VLAN.
    /// </summary>
    public string? MskVlan { get; set; }

    /// <summary>
    /// Gets or sets the business unit.
    /// </summary>
    public string? BusinessUnit { get; set; }

    /// <summary>
    /// Gets or sets the hygiene lite status.
    /// </summary>
    public string? Hygienelite { get; set; }

    /// <summary>
    /// Gets or sets the maintenance schedule.
    /// </summary>
    public string? MaintenanceSchedule { get; set; }

    /// <summary>
    /// Gets or sets the application manager.
    /// </summary>
    public string? ApplicationManager { get; set; }

    /// <summary>
    /// Gets or sets the cost center.
    /// </summary>
    public string? CostCenter { get; set; }

    /// <summary>
    /// Gets or sets the attested by.
    /// </summary>
    public string? AttestedBy { get; set; }

    /// <summary>
    /// Gets or sets the DNS domain.
    /// </summary>
    public string? DnsDomain { get; set; }

    /// <summary>
    /// Gets or sets the MSK clinical service 1.
    /// </summary>
    public string? MskClinsvc1 { get; set; }

    /// <summary>
    /// Gets or sets the assigned status.
    /// </summary>
    public string? Assigned { get; set; }

    /// <summary>
    /// Gets or sets the acquisition method.
    /// </summary>
    public string? AcquisitionMethod { get; set; }

    /// <summary>
    /// Gets or sets the life cycle stage link value.
    /// </summary>
    public string? LifeCycleStage { get; set; }

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the MSK monitor size.
    /// </summary>
    public string? MskMonitorSize { get; set; }

    /// <summary>
    /// Gets or sets the MSK management MAC address.
    /// </summary>
    public string? MskMgmtmac { get; set; }

    /// <summary>
    /// Gets or sets the MSK multiboot status.
    /// </summary>
    public string? MskMultiboot { get; set; }

    /// <summary>
    /// Gets or sets the last change date.
    /// </summary>
    public string? LastChangeDate { get; set; }

    /// <summary>
    /// Gets or sets the short description.
    /// </summary>
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets the image.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the report top use EO.
    /// </summary>
    public string? RptTopluseo { get; set; }

    /// <summary>
    /// Gets or sets the managed by.
    /// </summary>
    public string? ManagedBy { get; set; }

    /// <summary>
    /// Gets or sets the accessibility level.
    /// </summary>
    public string? AccessibilityLevel { get; set; }

    /// <summary>
    /// Gets or sets the can print status.
    /// </summary>
    public string? CanPrint { get; set; }

    /// <summary>
    /// Gets or sets the last discovered date.
    /// </summary>
    public string? LastDiscovered { get; set; }

    /// <summary>
    /// Gets or sets the next assessment date.
    /// </summary>
    public string? NextAssessmentDate { get; set; }

    /// <summary>
    /// Gets or sets the system class name.
    /// </summary>
    public string? SysClassName { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the contract end date.
    /// </summary>
    public string? ContractEndDate { get; set; }

    /// <summary>
    /// Gets or sets the data classification.
    /// </summary>
    public string? DataClassification { get; set; }

    /// <summary>
    /// Gets or sets the life cycle stage status link value.
    /// </summary>
    public string? LifeCycleStageStatus { get; set; }

    /// <summary>
    /// Gets or sets the executive sponsor.
    /// </summary>
    public string? ExecutiveSponsor { get; set; }

    /// <summary>
    /// Gets or sets the vendor link value.
    /// </summary>
    public string? Vendor { get; set; }

    /// <summary>
    /// Gets or sets the special handling status.
    /// </summary>
    public string? SpecialHandling { get; set; }

    /// <summary>
    /// Gets or sets the certified status.
    /// </summary>
    public string? Certified { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets the model number.
    /// </summary>
    public string? ModelNumber { get; set; }

    /// <summary>
    /// Gets or sets the assigned to.
    /// </summary>
    public string? AssignedTo { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the MSK reconciliation identity.
    /// </summary>
    public string? MskReconciliationIdentity { get; set; }

    /// <summary>
    /// Gets or sets the processes stores MSK data or used to support MSK business function status.
    /// </summary>
    public string? ProcessesStoresMskDataOrUsedToSupportMskBusinessFunction { get; set; }

    /// <summary>
    /// Gets or sets the application category link value.
    /// </summary>
    public string? ApplicationCategoryValue { get; set; }

    /// <summary>
    /// Gets or sets the business criticality.
    /// </summary>
    public string? BusinessCriticality { get; set; }

    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the age in months.
    /// </summary>
    public string? AgeInMonth { get; set; }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the application aliases.
    /// </summary>
    public string? ApplicationAliases { get; set; }

    /// <summary>
    /// Gets or sets the support group.
    /// </summary>
    public string? SupportGroup { get; set; }

    /// <summary>
    /// Gets or sets the room.
    /// </summary>
    public string? Room { get; set; }

    /// <summary>
    /// Gets or sets the technology stack.
    /// </summary>
    public string? TechnologyStack { get; set; }

    /// <summary>
    /// Gets or sets the audience type.
    /// </summary>
    public string? AudienceType { get; set; }

    /// <summary>
    /// Gets or sets the planned disposition.
    /// </summary>
    public string? PlannedDisposition { get; set; }

    /// <summary>
    /// Gets or sets the to review status.
    /// </summary>
    public string? ToReview { get; set; }

    /// <summary>
    /// Gets or sets the correlation ID.
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the unverified status.
    /// </summary>
    public string? Unverified { get; set; }

    /// <summary>
    /// Gets or sets the attributes.
    /// </summary>
    public string? Attributes { get; set; }

    /// <summary>
    /// Gets or sets the jack.
    /// </summary>
    public string? Jack { get; set; }

    /// <summary>
    /// Gets or sets the target business application.
    /// </summary>
    public string? TargetBusinessApp { get; set; }

    /// <summary>
    /// Gets or sets the asset.
    /// </summary>
    public string? Asset { get; set; }

    /// <summary>
    /// Gets or sets the last WSID.
    /// </summary>
    public string? LastWsid { get; set; }

    /// <summary>
    /// Gets or sets the software license.
    /// </summary>
    public string? SoftwareLicense { get; set; }

    /// <summary>
    /// Gets or sets the patch status.
    /// </summary>
    public string? PatchStatus { get; set; }

    /// <summary>
    /// Gets or sets the skip sync status.
    /// </summary>
    public string? SkipSync { get; set; }

    /// <summary>
    /// Gets or sets the product instance ID.
    /// </summary>
    public string? ProductInstanceId { get; set; }

    /// <summary>
    /// Gets or sets the active user count.
    /// </summary>
    public string? ActiveUserCount { get; set; }

    /// <summary>
    /// Gets or sets the support vendor.
    /// </summary>
    public string? SupportVendor { get; set; }

    /// <summary>
    /// Gets or sets the baseline risk.
    /// </summary>
    public string? BaselineRisk { get; set; }

    /// <summary>
    /// Gets or sets the appraisal fiscal type.
    /// </summary>
    public string? AppraisalFiscalType { get; set; }

    /// <summary>
    /// Gets or sets the attestation score.
    /// </summary>
    public string? AttestationScore { get; set; }

    /// <summary>
    /// Gets or sets the processes stores regulated data data which requires protection by law or contract status.
    /// </summary>
    public string? ProcessesStoresRegulatedDataDataWhichRequiresProtectionByLawOrContr { get; set; }

    /// <summary>
    /// Gets or sets the system updated by.
    /// </summary>
    public string? SysUpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the system created on date.
    /// </summary>
    public string? SysCreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the system domain link value.
    /// </summary>
    public string? SysDomainValue { get; set; }

    /// <summary>
    /// Gets or sets the install date.
    /// </summary>
    public string? InstallDate { get; set; }

    /// <summary>
    /// Gets or sets the asset tag.
    /// </summary>
    public string? AssetTag { get; set; }

    /// <summary>
    /// Gets or sets the work notes.
    /// </summary>
    public string? UWorkNotes { get; set; }

    /// <summary>
    /// Gets or sets the FQDN.
    /// </summary>
    public string? Fqdn { get; set; }

    /// <summary>
    /// Gets or sets the published status.
    /// </summary>
    public string? Published { get; set; }

    /// <summary>
    /// Gets or sets the change control.
    /// </summary>
    public string? ChangeControl { get; set; }

    /// <summary>
    /// Gets or sets the emergency tier.
    /// </summary>
    public string? EmergencyTier { get; set; }

    /// <summary>
    /// Gets or sets the overall status.
    /// </summary>
    public string? OverallStatus { get; set; }

    /// <summary>
    /// Gets or sets the product support status.
    /// </summary>
    public string? ProductSupportStatus { get; set; }

    /// <summary>
    /// Gets or sets the delivery date.
    /// </summary>
    public string? DeliveryDate { get; set; }

    /// <summary>
    /// Gets or sets the install status.
    /// </summary>
    public string? InstallStatus { get; set; }

    /// <summary>
    /// Gets or sets the supported by.
    /// </summary>
    public string? SupportedBy { get; set; }

    /// <summary>
    /// Gets or sets the MSK instance ID.
    /// </summary>
    public string? MskInstanceId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the impacted departments.
    /// </summary>
    public string? ImpactedDepartments { get; set; }

    /// <summary>
    /// Gets or sets the subcategory.
    /// </summary>
    public string? Subcategory { get; set; }

    /// <summary>
    /// Gets or sets the work notes.
    /// </summary>
    public string? WorkNotes { get; set; }

    /// <summary>
    /// Gets or sets the assignment group.
    /// </summary>
    public string? AssignmentGroup { get; set; }

    /// <summary>
    /// Gets or sets the application type.
    /// </summary>
    public string? ApplicationType { get; set; }

    /// <summary>
    /// Gets or sets the architecture type.
    /// </summary>
    public string? ArchitectureType { get; set; }

    /// <summary>
    /// Gets or sets the report month.
    /// </summary>
    public string? RptMonth { get; set; }

    /// <summary>
    /// Gets or sets the managed by group.
    /// </summary>
    public string? ManagedByGroup { get; set; }

    /// <summary>
    /// Gets or sets the MSK port.
    /// </summary>
    public string? MskPort { get; set; }

    /// <summary>
    /// Gets or sets the platform.
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// Gets or sets the MSK area.
    /// </summary>
    public string? MskArea { get; set; }

    /// <summary>
    /// Gets or sets the system ID.
    /// </summary>
    public string? SysId { get; set; }

    /// <summary>
    /// Gets or sets the PO number.
    /// </summary>
    public string? PoNumber { get; set; }

    /// <summary>
    /// Gets or sets the IMEI number.
    /// </summary>
    public string? ImeiNumber { get; set; }

    /// <summary>
    /// Gets or sets the checked in status.
    /// </summary>
    public string? CheckedIn { get; set; }

    /// <summary>
    /// Gets or sets the system class path.
    /// </summary>
    public string? SysClassPath { get; set; }

    /// <summary>
    /// Gets or sets the MAC address.
    /// </summary>
    public string? MacAddress { get; set; }

    /// <summary>
    /// Gets or sets the company.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Gets or sets the justification.
    /// </summary>
    public string? Justification { get; set; }

    /// <summary>
    /// Gets or sets the retired date.
    /// </summary>
    public string? RetiredDate { get; set; }

    /// <summary>
    /// Gets or sets the department.
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// Gets or sets the remedy notes log.
    /// </summary>
    public string? RemedyNotesLog { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public string? UNumber { get; set; }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the cost.
    /// </summary>
    public string? Cost { get; set; }

    /// <summary>
    /// Gets or sets the CMDB software product model.
    /// </summary>
    public string? CmdbSoftwareProductModel { get; set; }

    /// <summary>
    /// Gets or sets the attestation status.
    /// </summary>
    public string? AttestationStatus { get; set; }

    /// <summary>
    /// Gets or sets the migration strategy.
    /// </summary>
    public string? MigrationStrategy { get; set; }

    /// <summary>
    /// Gets or sets the version status.
    /// </summary>
    public string? VersionStatus { get; set; }

    /// <summary>
    /// Gets or sets the floor.
    /// </summary>
    public string? Floor { get; set; }

    /// <summary>
    /// Gets or sets the system modification count.
    /// </summary>
    public string? SysModCount { get; set; }

    /// <summary>
    /// Gets or sets the monitor.
    /// </summary>
    public string? Monitor { get; set; }

    /// <summary>
    /// Gets or sets the IP address.
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the model ID.
    /// </summary>
    public string? ModelId { get; set; }

    /// <summary>
    /// Gets or sets the duplicate of.
    /// </summary>
    public string? DuplicateOf { get; set; }

    /// <summary>
    /// Gets or sets the system tags.
    /// </summary>
    public string? SysTags { get; set; }

    /// <summary>
    /// Gets or sets the cost CC.
    /// </summary>
    public string? CostCc { get; set; }

    /// <summary>
    /// Gets or sets the remedy audits.
    /// </summary>
    public string? RemedyAudits { get; set; }

    /// <summary>
    /// Gets or sets the order date.
    /// </summary>
    public string? OrderDate { get; set; }

    /// <summary>
    /// Gets or sets the schedule.
    /// </summary>
    public string? Schedule { get; set; }

    /// <summary>
    /// Gets or sets the user base.
    /// </summary>
    public string? UserBase { get; set; }

    /// <summary>
    /// Gets or sets the environment.
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Gets or sets the is externally facing status.
    /// </summary>
    public string? IsExternallyFacing { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public string? Due { get; set; }

    /// <summary>
    /// Gets or sets the attested status.
    /// </summary>
    public string? Attested { get; set; }

    /// <summary>
    /// Gets or sets the platform host.
    /// </summary>
    public string? PlatformHost { get; set; }

    /// <summary>
    /// Gets or sets the MSK MAC address 2.
    /// </summary>
    public string? MskMacAddress2 { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the MSK asset ID.
    /// </summary>
    public string? MskAssetId { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the fault count.
    /// </summary>
    public string? FaultCount { get; set; }

    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    public string? Age { get; set; }

    /// <summary>
    /// Gets or sets the lease ID.
    /// </summary>
    public string? LeaseId { get; set; }

    /// <summary>
    /// Gets or sets the audit checkpoint.
    /// </summary>
    public string? AuditCheckpoint { get; set; }

    /// <summary>
    /// Gets or sets the MSK MAC address 3.
    /// </summary>
    public string? MskMacAddress3 { get; set; }
}