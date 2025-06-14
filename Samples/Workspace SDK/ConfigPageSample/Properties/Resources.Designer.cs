// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Genetec.Dap.CodeSamples.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to Battery expiration date.
        /// </summary>
        public static string BatteryExpirationDate {
            get {
                return ResourceManager.GetString("BatteryExpirationDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AED Unit.
        /// </summary>
        public static string CustomEntityTypeName {
            get {
                return ResourceManager.GetString("CustomEntityTypeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last inspection date.
        /// </summary>
        public static string LastInspectionDate {
            get {
                return ResourceManager.GetString("LastInspectionDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Next scheduled maintenance.
        /// </summary>
        public static string NextScheduledMaintenance {
            get {
                return ResourceManager.GetString("NextScheduledMaintenance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pad expiration date.
        /// </summary>
        public static string PadExpirationDate {
            get {
                return ResourceManager.GetString("PadExpirationDate", resourceCulture);
            }
        }
    }
}
