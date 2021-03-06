﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dmarc.AggregateReport.Parser.Lambda.Dao {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AggregateReportParserDaoResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal AggregateReportParserDaoResources() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Dmarc.AggregateReport.Parser.Lambda.Dao.AggregateReportParserDaoResources", typeof(AggregateReportParserDaoResources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to  INSERT INTO `aggregate_report`
        ///(`request_id`,
        ///`original_uri`,
        ///`attachment_filename`,
        ///`version`,
        ///`org_name`,
        ///`email`,
        ///`report_id`,
        ///`extra_contact_info`,
        ///`effective_date`,
        ///`begin_date`,
        ///`end_date`,
        ///`domain`,
        ///`adkim`,
        ///`aspf`,
        ///`p`,
        ///`sp`,
        ///`pct`,
        ///`fo`,
        ///`created_date`)
        ///VALUES
        ///(
        ///@request_id,
        ///@original_uri,
        ///@attachment_filename,
        ///@version,
        ///@org_name,
        ///@email,
        ///@report_id,
        ///@extra_contact_info,
        ///@effective_date,
        ///@begin_date,
        ///@end_date,
        ///@domain,
        ///@adkim,
        ///@aspf,
        ///@p,
        ///@sp,
        ///@pct,
        ///@fo,
        ///@cr [rest of string was truncated]&quot;;.
        /// </summary>
        public static string InsertAggregateReport {
            get {
                return ResourceManager.GetString("InsertAggregateReport", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to INSERT INTO `dmarc`.`dkim_auth_result`(`record_id`,`domain`,`selector`,`dkim_result`,`human_result`) VALUES.
        /// </summary>
        public static string InsertDkimAuthResult {
            get {
                return ResourceManager.GetString("InsertDkimAuthResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to (@a{0},@b{0},@c{0},@d{0},@e{0}).
        /// </summary>
        public static string InsertDkimAuthResultValueFormatString {
            get {
                return ResourceManager.GetString("InsertDkimAuthResultValueFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to INSERT INTO `policy_override_reason` (`record_id`,`policy_override`,`comment`) VALUES 
        ///
        ///.
        /// </summary>
        public static string InsertPolicyOverrideReason {
            get {
                return ResourceManager.GetString("InsertPolicyOverrideReason", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to (@a{0},@b{0},@c{0}).
        /// </summary>
        public static string InsertPolicyOverrideReasonValueFormatString {
            get {
                return ResourceManager.GetString("InsertPolicyOverrideReasonValueFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to INSERT INTO`record`(`aggregate_report_id`,`source_ip`,`count`,`disposition`,`dkim`,`spf`,`envelope_to`,`envelope_from`,`header_from`) VALUES.
        /// </summary>
        public static string InsertRecord {
            get {
                return ResourceManager.GetString("InsertRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to (@a{0},@b{0},@c{0},@d{0},@e{0},@f{0},@g{0},@h{0},@i{0}).
        /// </summary>
        public static string InsertRecordValueFormatString {
            get {
                return ResourceManager.GetString("InsertRecordValueFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to INSERT INTO `spf_auth_result`(`record_id`,`domain`,`scope`,`spf_result`) VALUES 
        ///.
        /// </summary>
        public static string InsertSpfAuthResult {
            get {
                return ResourceManager.GetString("InsertSpfAuthResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to (@a{0},@b{0},@c{0},@d{0}).
        /// </summary>
        public static string InsertSpfAuthResultValueFormatString {
            get {
                return ResourceManager.GetString("InsertSpfAuthResultValueFormatString", resourceCulture);
            }
        }
    }
}
