﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dmarc.Admin.Api.Dao.Group {
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
    public class GroupDaoResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal GroupDaoResources() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Dmarc.Admin.Api.Dao.Group.GroupDaoResources", typeof(GroupDaoResources).GetTypeInfo().Assembly);
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
        ///    Looks up a localized string similar to INSERT INTO `group` (name)
        ///VALUES
        ///(@name) ON DUPLICATE KEY UPDATE id=LAST_INSERT_ID(id);
        ///SELECT LAST_INSERT_ID();.
        /// </summary>
        public static string InsertGroup {
            get {
                return ResourceManager.GetString("InsertGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to SELECT id, name 
        ///FROM `group`
        ///WHERE id = @id;.
        /// </summary>
        public static string SelectGroupById {
            get {
                return ResourceManager.GetString("SelectGroupById", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to SELECT 
        ///g.id, 
        ///g.name 
        ///FROM `group` g
        ///JOIN group_domain_mapping gdm ON g.id = gdm.group_id
        ///JOIN domain d on d.id  = gdm.domain_id
        ///WHERE d.id = @domainId
        ///AND (@search IS NULL OR LOWER(g.name) LIKE CONCAT(LOWER(@search), &apos;%&apos;))
        ///ORDER BY d.name
        ///LIMIT @offset, @pageSize;.
        /// </summary>
        public static string SelectGroupsByDomainId {
            get {
                return ResourceManager.GetString("SelectGroupsByDomainId", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to SELECT 
        ///g1.id,
        ///g1.name
        ///FROM
        ///(
        ///
        ///(SELECT 
        ///g.id,
        ///g.name
        ///FROM `group` g
        ///WHERE (@search IS NULL OR LOWER(g.name) LIKE CONCAT(&apos;%&apos;, LOWER(@search), &apos;%&apos;))
        ///ORDER BY g.name
        ///LIMIT 0, @limit)
        ///
        ///UNION
        ///
        ///(SELECT 
        ///g.id,
        ///g.name
        ///FROM `group` g
        ///WHERE g.id IN ({0}))
        ///) g1
        ///ORDER by g1.name.
        /// </summary>
        public static string SelectGroupsByName {
            get {
                return ResourceManager.GetString("SelectGroupsByName", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to SELECT 
        ///g.id, 
        ///g.name 
        ///FROM `group` g
        ///JOIN group_user_mapping gum ON g.id = gum.group_id
        ///JOIN user u on u.id  = gum.user_id
        ///WHERE u.id = @userId
        ///AND (@search IS NULL OR g.name LIKE CONCAT(@search, &apos;%&apos;))
        ///ORDER BY g.name
        ///LIMIT @offset, @pageSize;.
        /// </summary>
        public static string SelectGroupsByUserId {
            get {
                return ResourceManager.GetString("SelectGroupsByUserId", resourceCulture);
            }
        }
    }
}
