using System.Text.Json.Serialization;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents a ServiceNow application entity.
/// </summary>
public class ServiceNowApplicationsJsonUnusable
{
    /// <summary>
    /// Gets or sets the attested date.
    /// </summary>
    [JsonPropertyName("attested_date")]
    public string? AttestedDate { get; set; }

    /// <summary>
    /// Gets or sets the application complexity.
    /// </summary>
    [JsonPropertyName("u_application_complexity")]
    public string? ApplicationComplexity { get; set; }

    /// <summary>
    /// Gets or sets the operational status.
    /// </summary>
    [JsonPropertyName("operational_status")]
    public string? OperationalStatus { get; set; }

    /// <summary>
    /// Gets or sets the AI capability.
    /// </summary>
    [JsonPropertyName("u_ai_capability")]
    public string? AiCapability { get; set; }

    /// <summary>
    /// Gets or sets the reasoning.
    /// </summary>
    [JsonPropertyName("reasoning")]
    public string? Reasoning { get; set; }

    /// <summary>
    /// Gets or sets the system updated on date.
    /// </summary>
    [JsonPropertyName("sys_updated_on")]
    public string? SysUpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the department code.
    /// </summary>
    [JsonPropertyName("u_department_code")]
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// Gets or sets the install type.
    /// </summary>
    [JsonPropertyName("install_type")]
    public string? InstallType { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Gets or sets the discovery source.
    /// </summary>
    [JsonPropertyName("discovery_source")]
    public string? DiscoverySource { get; set; }

    /// <summary>
    /// Gets or sets the first discovered date.
    /// </summary>
    [JsonPropertyName("first_discovered")]
    public string? FirstDiscovered { get; set; }

    /// <summary>
    /// Gets or sets the due in date.
    /// </summary>
    [JsonPropertyName("due_in")]
    public string? DueIn { get; set; }

    /// <summary>
    /// Gets or sets the MSK ADDM key.
    /// </summary>
    [JsonPropertyName("u_msk_addm_key")]
    public string? MskAddmKey { get; set; }

    /// <summary>
    /// Gets or sets the IT application owner.
    /// </summary>
    [JsonPropertyName("it_application_owner")]
    public LinkValue? ItApplicationOwner { get; set; }

    /// <summary>
    /// Gets or sets the APM business process.
    /// </summary>
    [JsonPropertyName("apm_business_process")]
    public string? ApmBusinessProcess { get; set; }

    /// <summary>
    /// Gets or sets the GL account.
    /// </summary>
    [JsonPropertyName("gl_account")]
    public string? GlAccount { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    [JsonPropertyName("invoice_number")]
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Gets or sets the system created by.
    /// </summary>
    [JsonPropertyName("sys_created_by")]
    public string? SysCreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the warranty expiration date.
    /// </summary>
    [JsonPropertyName("warranty_expiration")]
    public string? WarrantyExpiration { get; set; }

    /// <summary>
    /// Gets or sets the organization unit count.
    /// </summary>
    [JsonPropertyName("organization_unit_count")]
    public string? OrganizationUnitCount { get; set; }

    /// <summary>
    /// Gets or sets the annual cost.
    /// </summary>
    [JsonPropertyName("u_annual_cost")]
    public string? AnnualCost { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application is active.
    /// </summary>
    [JsonPropertyName("active")]
    public string? Active { get; set; }

    /// <summary>
    /// Gets or sets the owned by link value.
    /// </summary>
    [JsonPropertyName("owned_by")]
    public LinkValue? OwnedBy { get; set; }

    /// <summary>
    /// Gets or sets the checked out status.
    /// </summary>
    [JsonPropertyName("checked_out")]
    public string? CheckedOut { get; set; }

    /// <summary>
    /// Gets or sets the report year.
    /// </summary>
    [JsonPropertyName("u_rpt_year")]
    public string? RptYear { get; set; }

    /// <summary>
    /// Gets or sets the system domain path.
    /// </summary>
    [JsonPropertyName("sys_domain_path")]
    public string? SysDomainPath { get; set; }

    /// <summary>
    /// Gets or sets the MSK VLAN.
    /// </summary>
    [JsonPropertyName("u_msk_vlan")]
    public string? MskVlan { get; set; }

    /// <summary>
    /// Gets or sets the business unit.
    /// </summary>
    [JsonPropertyName("business_unit")]
    public string? BusinessUnit { get; set; }

    /// <summary>
    /// Gets or sets the hygiene lite status.
    /// </summary>
    [JsonPropertyName("u_hygienelite")]
    public string? Hygienelite { get; set; }

    /// <summary>
    /// Gets or sets the maintenance schedule.
    /// </summary>
    [JsonPropertyName("maintenance_schedule")]
    public string? MaintenanceSchedule { get; set; }

    /// <summary>
    /// Gets or sets the application manager.
    /// </summary>
    [JsonPropertyName("application_manager")]
    public string? ApplicationManager { get; set; }

    /// <summary>
    /// Gets or sets the cost center.
    /// </summary>
    [JsonPropertyName("cost_center")]
    public string? CostCenter { get; set; }

    /// <summary>
    /// Gets or sets the attested by.
    /// </summary>
    [JsonPropertyName("attested_by")]
    public string? AttestedBy { get; set; }

    /// <summary>
    /// Gets or sets the DNS domain.
    /// </summary>
    [JsonPropertyName("dns_domain")]
    public string? DnsDomain { get; set; }

    /// <summary>
    /// Gets or sets the MSK clinical service 1.
    /// </summary>
    [JsonPropertyName("u_msk_clinsvc1")]
    public string? MskClinsvc1 { get; set; }

    /// <summary>
    /// Gets or sets the assigned status.
    /// </summary>
    [JsonPropertyName("assigned")]
    public string? Assigned { get; set; }

    /// <summary>
    /// Gets or sets the acquisition method.
    /// </summary>
    [JsonPropertyName("u_acquisition_method")]
    public string? AcquisitionMethod { get; set; }

    /// <summary>
    /// Gets or sets the life cycle stage link value.
    /// </summary>
    [JsonPropertyName("life_cycle_stage")]
    public LinkValue? LifeCycleStage { get; set; }

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    [JsonPropertyName("purchase_date")]
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the MSK monitor size.
    /// </summary>
    [JsonPropertyName("u_msk_monitor_size")]
    public string? MskMonitorSize { get; set; }

    /// <summary>
    /// Gets or sets the MSK management MAC address.
    /// </summary>
    [JsonPropertyName("u_msk_mgmtmac")]
    public string? MskMgmtmac { get; set; }

    /// <summary>
    /// Gets or sets the MSK multiboot status.
    /// </summary>
    [JsonPropertyName("u_msk_multiboot")]
    public string? MskMultiboot { get; set; }

    /// <summary>
    /// Gets or sets the last change date.
    /// </summary>
    [JsonPropertyName("last_change_date")]
    public string? LastChangeDate { get; set; }

    /// <summary>
    /// Gets or sets the short description.
    /// </summary>
    [JsonPropertyName("short_description")]
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets the image.
    /// </summary>
    [JsonPropertyName("u_image")]
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the report top use EO.
    /// </summary>
    [JsonPropertyName("u_rpt_topluseo")]
    public string? RptTopluseo { get; set; }

    /// <summary>
    /// Gets or sets the managed by.
    /// </summary>
    [JsonPropertyName("managed_by")]
    public string? ManagedBy { get; set; }

    /// <summary>
    /// Gets or sets the accessibility level.
    /// </summary>
    [JsonPropertyName("accessibility_level")]
    public string? AccessibilityLevel { get; set; }

    /// <summary>
    /// Gets or sets the can print status.
    /// </summary>
    [JsonPropertyName("can_print")]
    public string? CanPrint { get; set; }

    /// <summary>
    /// Gets or sets the last discovered date.
    /// </summary>
    [JsonPropertyName("last_discovered")]
    public string? LastDiscovered { get; set; }

    /// <summary>
    /// Gets or sets the next assessment date.
    /// </summary>
    [JsonPropertyName("next_assessment_date")]
    public string? NextAssessmentDate { get; set; }

    /// <summary>
    /// Gets or sets the system class name.
    /// </summary>
    [JsonPropertyName("sys_class_name")]
    public string? SysClassName { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the contract end date.
    /// </summary>
    [JsonPropertyName("contract_end_date")]
    public string? ContractEndDate { get; set; }

    /// <summary>
    /// Gets or sets the data classification.
    /// </summary>
    [JsonPropertyName("data_classification")]
    public string? DataClassification { get; set; }

    /// <summary>
    /// Gets or sets the life cycle stage status link value.
    /// </summary>
    [JsonPropertyName("life_cycle_stage_status")]
    public LinkValue? LifeCycleStageStatus { get; set; }

    /// <summary>
    /// Gets or sets the vendor link value.
    /// </summary>
    [JsonPropertyName("vendor")]
    public LinkValue? Vendor { get; set; }

    /// <summary>
    /// Gets or sets the special handling status.
    /// </summary>
    [JsonPropertyName("u_special_handling")]
    public string? SpecialHandling { get; set; }

    /// <summary>
    /// Gets or sets the certified status.
    /// </summary>
    [JsonPropertyName("certified")]
    public string? Certified { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets the model number.
    /// </summary>
    [JsonPropertyName("model_number")]
    public string? ModelNumber { get; set; }

    /// <summary>
    /// Gets or sets the assigned to.
    /// </summary>
    [JsonPropertyName("assigned_to")]
    public string? AssignedTo { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the MSK reconciliation identity.
    /// </summary>
    [JsonPropertyName("u_msk_reconciliation_identity")]
    public string? MskReconciliationIdentity { get; set; }

    /// <summary>
    /// Gets or sets the processes stores MSK data or used to support MSK business function status.
    /// </summary>
    [JsonPropertyName("u_processes_stores_msk_data_or_used_to_support_msk_business_function")]
    public string? ProcessesStoresMskDataOrUsedToSupportMskBusinessFunction { get; set; }

    /// <summary>
    /// Gets or sets the application category link value.
    /// </summary>
    [JsonPropertyName("application_category")]
    public LinkValue? ApplicationCategory { get; set; }

    /// <summary>
    /// Gets or sets the business criticality.
    /// </summary>
    [JsonPropertyName("business_criticality")]
    public string? BusinessCriticality { get; set; }

    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the age in months.
    /// </summary>
    [JsonPropertyName("age_in_month")]
    public string? AgeInMonth { get; set; }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the application aliases.
    /// </summary>
    [JsonPropertyName("u_application_aliases")]
    public string? ApplicationAliases { get; set; }

    /// <summary>
    /// Gets or sets the support group.
    /// </summary>
    [JsonPropertyName("support_group")]
    public LinkValue? SupportGroup { get; set; }

    /// <summary>
    /// Gets or sets the room.
    /// </summary>
    [JsonPropertyName("u_room")]
    public string? Room { get; set; }

    /// <summary>
    /// Gets or sets the technology stack.
    /// </summary>
    [JsonPropertyName("technology_stack")]
    public string? TechnologyStack { get; set; }

    /// <summary>
    /// Gets or sets the audience type.
    /// </summary>
    [JsonPropertyName("audience_type")]
    public string? AudienceType { get; set; }

    /// <summary>
    /// Gets or sets the planned disposition.
    /// </summary>
    [JsonPropertyName("planned_disposition")]
    public string? PlannedDisposition { get; set; }

    /// <summary>
    /// Gets or sets the to review status.
    /// </summary>
    [JsonPropertyName("u_to_review")]
    public string? ToReview { get; set; }

    /// <summary>
    /// Gets or sets the correlation ID.
    /// </summary>
    [JsonPropertyName("correlation_id")]
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the unverified status.
    /// </summary>
    [JsonPropertyName("unverified")]
    public string? Unverified { get; set; }

    /// <summary>
    /// Gets or sets the attributes.
    /// </summary>
    [JsonPropertyName("attributes")]
    public string? Attributes { get; set; }

    /// <summary>
    /// Gets or sets the jack.
    /// </summary>
    [JsonPropertyName("u_jack")]
    public string? Jack { get; set; }

    /// <summary>
    /// Gets or sets the target business application.
    /// </summary>
    [JsonPropertyName("target_business_app")]
    public string? TargetBusinessApp { get; set; }

    /// <summary>
    /// Gets or sets the asset.
    /// </summary>
    [JsonPropertyName("asset")]
    public string? Asset { get; set; }

    /// <summary>
    /// Gets or sets the last WSID.
    /// </summary>
    [JsonPropertyName("u_last_wsid")]
    public string? LastWsid { get; set; }

    /// <summary>
    /// Gets or sets the software license.
    /// </summary>
    [JsonPropertyName("software_license")]
    public string? SoftwareLicense { get; set; }

    /// <summary>
    /// Gets or sets the patch status.
    /// </summary>
    [JsonPropertyName("u_patch_status")]
    public string? PatchStatus { get; set; }

    /// <summary>
    /// Gets or sets the skip sync status.
    /// </summary>
    [JsonPropertyName("skip_sync")]
    public string? SkipSync { get; set; }

    /// <summary>
    /// Gets or sets the product instance ID.
    /// </summary>
    [JsonPropertyName("product_instance_id")]
    public string? ProductInstanceId { get; set; }

    /// <summary>
    /// Gets or sets the active user count.
    /// </summary>
    [JsonPropertyName("active_user_count")]
    public string? ActiveUserCount { get; set; }

    /// <summary>
    /// Gets or sets the support vendor.
    /// </summary>
    [JsonPropertyName("support_vendor")]
    public string? SupportVendor { get; set; }

    /// <summary>
    /// Gets or sets the baseline risk.
    /// </summary>
    [JsonPropertyName("u_baseline_risk")]
    public string? BaselineRisk { get; set; }

    /// <summary>
    /// Gets or sets the appraisal fiscal type.
    /// </summary>
    [JsonPropertyName("appraisal_fiscal_type")]
    public string? AppraisalFiscalType { get; set; }

    /// <summary>
    /// Gets or sets the attestation score.
    /// </summary>
    [JsonPropertyName("attestation_score")]
    public string? AttestationScore { get; set; }

    /// <summary>
    /// Gets or sets the processes stores regulated data data which requires protection by law or contract status.
    /// </summary>
    [JsonPropertyName("u_processes_stores_regulated_data_data_which_requires_protection_by_law_or_contr")]
    public string? ProcessesStoresRegulatedDataDataWhichRequiresProtectionByLawOrContr { get; set; }

    /// <summary>
    /// Gets or sets the system updated by.
    /// </summary>
    [JsonPropertyName("sys_updated_by")]
    public string? SysUpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the system created on date.
    /// </summary>
    [JsonPropertyName("sys_created_on")]
    public string? SysCreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the system domain link value.
    /// </summary>
    [JsonPropertyName("sys_domain")]
    public LinkValue? SysDomain { get; set; }

    /// <summary>
    /// Gets or sets the install date.
    /// </summary>
    [JsonPropertyName("install_date")]
    public string? InstallDate { get; set; }

    /// <summary>
    /// Gets or sets the asset tag.
    /// </summary>
    [JsonPropertyName("asset_tag")]
    public string? AssetTag { get; set; }

    /// <summary>
    /// Gets or sets the work notes.
    /// </summary>
    [JsonPropertyName("u_work_notes")]
    public string? UWorkNotes { get; set; }

    /// <summary>
    /// Gets or sets the FQDN.
    /// </summary>
    [JsonPropertyName("fqdn")]
    public string? Fqdn { get; set; }

    /// <summary>
    /// Gets or sets the published status.
    /// </summary>
    [JsonPropertyName("u_published")]
    public string? Published { get; set; }

    /// <summary>
    /// Gets or sets the change control.
    /// </summary>
    [JsonPropertyName("change_control")]
    public string? ChangeControl { get; set; }

    /// <summary>
    /// Gets or sets the emergency tier.
    /// </summary>
    [JsonPropertyName("emergency_tier")]
    public string? EmergencyTier { get; set; }

    /// <summary>
    /// Gets or sets the overall status.
    /// </summary>
    [JsonPropertyName("u_overall_status")]
    public string? OverallStatus { get; set; }

    /// <summary>
    /// Gets or sets the product support status.
    /// </summary>
    [JsonPropertyName("product_support_status")]
    public string? ProductSupportStatus { get; set; }

    /// <summary>
    /// Gets or sets the delivery date.
    /// </summary>
    [JsonPropertyName("delivery_date")]
    public string? DeliveryDate { get; set; }

    /// <summary>
    /// Gets or sets the install status.
    /// </summary>
    [JsonPropertyName("install_status")]
    public string? InstallStatus { get; set; }

    /// <summary>
    /// Gets or sets the supported by.
    /// </summary>
    [JsonPropertyName("supported_by")]
    public LinkValue? SupportedBy { get; set; }

    /// <summary>
    /// Gets or sets the MSK instance ID.
    /// </summary>
    [JsonPropertyName("u_msk_instance_id")]
    public string? MskInstanceId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the impacted departments.
    /// </summary>
    [JsonPropertyName("u_impacted_departments")]
    public string? ImpactedDepartments { get; set; }

    /// <summary>
    /// Gets or sets the subcategory.
    /// </summary>
    [JsonPropertyName("subcategory")]
    public string? Subcategory { get; set; }

    /// <summary>
    /// Gets or sets the work notes.
    /// </summary>
    [JsonPropertyName("work_notes")]
    public string? WorkNotes { get; set; }

    /// <summary>
    /// Gets or sets the assignment group.
    /// </summary>
    [JsonPropertyName("assignment_group")]
    public string? AssignmentGroup { get; set; }

    /// <summary>
    /// Gets or sets the application type.
    /// </summary>
    [JsonPropertyName("application_type")]
    public string? ApplicationType { get; set; }

    /// <summary>
    /// Gets or sets the architecture type.
    /// </summary>
    [JsonPropertyName("architecture_type")]
    public string? ArchitectureType { get; set; }

    /// <summary>
    /// Gets or sets the report month.
    /// </summary>
    [JsonPropertyName("u_rpt_month")]
    public string? RptMonth { get; set; }

    /// <summary>
    /// Gets or sets the managed by group.
    /// </summary>
    [JsonPropertyName("managed_by_group")]
    public string? ManagedByGroup { get; set; }

    /// <summary>
    /// Gets or sets the MSK port.
    /// </summary>
    [JsonPropertyName("u_msk_port")]
    public string? MskPort { get; set; }

    /// <summary>
    /// Gets or sets the platform.
    /// </summary>
    [JsonPropertyName("platform")]
    public string? Platform { get; set; }

    /// <summary>
    /// Gets or sets the MSK area.
    /// </summary>
    [JsonPropertyName("u_msk_area")]
    public string? MskArea { get; set; }

    /// <summary>
    /// Gets or sets the system ID.
    /// </summary>
    [JsonPropertyName("sys_id")]
    public string? SysId { get; set; }

    /// <summary>
    /// Gets or sets the PO number.
    /// </summary>
    [JsonPropertyName("po_number")]
    public string? PoNumber { get; set; }

    /// <summary>
    /// Gets or sets the IMEI number.
    /// </summary>
    [JsonPropertyName("u_imei_number")]
    public string? ImeiNumber { get; set; }

    /// <summary>
    /// Gets or sets the checked in status.
    /// </summary>
    [JsonPropertyName("checked_in")]
    public string? CheckedIn { get; set; }

    /// <summary>
    /// Gets or sets the system class path.
    /// </summary>
    [JsonPropertyName("sys_class_path")]
    public string? SysClassPath { get; set; }

    /// <summary>
    /// Gets or sets the MAC address.
    /// </summary>
    [JsonPropertyName("mac_address")]
    public string? MacAddress { get; set; }

    /// <summary>
    /// Gets or sets the company.
    /// </summary>
    [JsonPropertyName("company")]
    public string? Company { get; set; }

    /// <summary>
    /// Gets or sets the justification.
    /// </summary>
    [JsonPropertyName("justification")]
    public string? Justification { get; set; }

    /// <summary>
    /// Gets or sets the retired date.
    /// </summary>
    [JsonPropertyName("u_retired_date")]
    public string? RetiredDate { get; set; }

    /// <summary>
    /// Gets or sets the department.
    /// </summary>
    [JsonPropertyName("department")]
    public LinkValue? Department { get; set; }

    /// <summary>
    /// Gets or sets the remedy notes log.
    /// </summary>
    [JsonPropertyName("u_remedy_notes_log")]
    public string? RemedyNotesLog { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    [JsonPropertyName("u_number")]
    public string? UNumber { get; set; }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    [JsonPropertyName("comments")]
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the cost.
    /// </summary>
    [JsonPropertyName("cost")]
    public string? Cost { get; set; }

    /// <summary>
    /// Gets or sets the CMDB software product model.
    /// </summary>
    [JsonPropertyName("cmdb_software_product_model")]
    public string? CmdbSoftwareProductModel { get; set; }

    /// <summary>
    /// Gets or sets the attestation status.
    /// </summary>
    [JsonPropertyName("attestation_status")]
    public string? AttestationStatus { get; set; }

    /// <summary>
    /// Gets or sets the migration strategy.
    /// </summary>
    [JsonPropertyName("migration_strategy")]
    public string? MigrationStrategy { get; set; }

    /// <summary>
    /// Gets or sets the version status.
    /// </summary>
    [JsonPropertyName("u_version_status")]
    public string? VersionStatus { get; set; }

    /// <summary>
    /// Gets or sets the floor.
    /// </summary>
    [JsonPropertyName("u_floor")]
    public string? Floor { get; set; }

    /// <summary>
    /// Gets or sets the system modification count.
    /// </summary>
    [JsonPropertyName("sys_mod_count")]
    public string? SysModCount { get; set; }

    /// <summary>
    /// Gets or sets the monitor.
    /// </summary>
    [JsonPropertyName("monitor")]
    public string? Monitor { get; set; }

    /// <summary>
    /// Gets or sets the IP address.
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the model ID.
    /// </summary>
    [JsonPropertyName("model_id")]
    public string? ModelId { get; set; }

    /// <summary>
    /// Gets or sets the duplicate of.
    /// </summary>
    [JsonPropertyName("duplicate_of")]
    public string? DuplicateOf { get; set; }

    /// <summary>
    /// Gets or sets the system tags.
    /// </summary>
    [JsonPropertyName("sys_tags")]
    public string? SysTags { get; set; }

    /// <summary>
    /// Gets or sets the cost CC.
    /// </summary>
    [JsonPropertyName("cost_cc")]
    public string? CostCc { get; set; }

    /// <summary>
    /// Gets or sets the remedy audits.
    /// </summary>
    [JsonPropertyName("u_remedy_audits")]
    public string? RemedyAudits { get; set; }

    /// <summary>
    /// Gets or sets the order date.
    /// </summary>
    [JsonPropertyName("order_date")]
    public string? OrderDate { get; set; }

    /// <summary>
    /// Gets or sets the schedule.
    /// </summary>
    [JsonPropertyName("schedule")]
    public string? Schedule { get; set; }

    /// <summary>
    /// Gets or sets the user base.
    /// </summary>
    [JsonPropertyName("user_base")]
    public string? UserBase { get; set; }

    /// <summary>
    /// Gets or sets the environment.
    /// </summary>
    [JsonPropertyName("environment")]
    public string? Environment { get; set; }

    /// <summary>
    /// Gets or sets the is externally facing status.
    /// </summary>
    [JsonPropertyName("u_is_externally_facing")]
    public string? IsExternallyFacing { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    [JsonPropertyName("due")]
    public string? Due { get; set; }

    /// <summary>
    /// Gets or sets the attested status.
    /// </summary>
    [JsonPropertyName("attested")]
    public string? Attested { get; set; }

    /// <summary>
    /// Gets or sets the platform host.
    /// </summary>
    [JsonPropertyName("platform_host")]
    public string? PlatformHost { get; set; }

    /// <summary>
    /// Gets or sets the MSK MAC address 2.
    /// </summary>
    [JsonPropertyName("u_msk_mac_address_2")]
    public string? MskMacAddress2 { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the MSK asset ID.
    /// </summary>
    [JsonPropertyName("u_msk_asset_id")]
    public string? MskAssetId { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the fault count.
    /// </summary>
    [JsonPropertyName("fault_count")]
    public string? FaultCount { get; set; }

    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    [JsonPropertyName("age")]
    public string? Age { get; set; }

    /// <summary>
    /// Gets or sets the lease ID.
    /// </summary>
    [JsonPropertyName("lease_id")]
    public string? LeaseId { get; set; }

    /// <summary>
    /// Gets or sets the audit checkpoint.
    /// </summary>
    [JsonPropertyName("u_audit_checkpoint")]
    public string? AuditCheckpoint { get; set; }

    /// <summary>
    /// Gets or sets the MSK MAC address 3.
    /// </summary>
    [JsonPropertyName("u_msk_mac_address_3")]
    public string? MskMacAddress3 { get; set; }

    /// <summary>
    /// Gets or sets the executive sponsor.
    /// </summary>
    [JsonPropertyName("u_executive_sponsor")]
    public LinkValue? ExecutiveSponsor { get; set; }
}

/// <summary>
/// Represents a link value.
/// </summary>
public class LinkValueUnusable
{
    /// <summary>
    /// Gets or sets the link.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}