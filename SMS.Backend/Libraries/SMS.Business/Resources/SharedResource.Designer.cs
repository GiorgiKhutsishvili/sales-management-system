//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Business.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SharedResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SMS.Business.Resources.SharedResource", typeof(SharedResource).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to კონსულტანტი პირადი ნომრით: {0} უკვე დამატებულია..
        /// </summary>
        internal static string Errors_ConsultantAlreadyExists {
            get {
                return ResourceManager.GetString("Errors_ConsultantAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to აღნიშნული {0} იდენტიფიკატორით კონსულტანტი ვერ მოიძებნა..
        /// </summary>
        internal static string Errors_ConsultantIsNotFound {
            get {
                return ResourceManager.GetString("Errors_ConsultantIsNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to შეცდომა: კონსულტანტი არ შეიძლება წარმოადგენდეს თავისი თავის რეკომენდატორს..
        /// </summary>
        internal static string Errors_ConsultantRepresentsHisOwnRecommender {
            get {
                return ResourceManager.GetString("Errors_ConsultantRepresentsHisOwnRecommender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to აღნიშნული {0} იდენტიფიკატორით პროდუქტი ვერ მოიძებნა..
        /// </summary>
        internal static string Errors_ProductIsNotFound {
            get {
                return ResourceManager.GetString("Errors_ProductIsNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to პროდუქტის რაოდენობა არ შეიძლება იყოს ნულზე ნაკლები..
        /// </summary>
        internal static string Errors_ProductQtyShoulNotBeLessThanZero {
            get {
                return ResourceManager.GetString("Errors_ProductQtyShoulNotBeLessThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to აღნიშნული {0} იდენტიფიკატორით რეკომენდატორი ვერ მოიძებნა..
        /// </summary>
        internal static string Errors_RecommendatorNotFound {
            get {
                return ResourceManager.GetString("Errors_RecommendatorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to გაყიდული პროდუქტი არ არსებობს..
        /// </summary>
        internal static string Errors_SaleIsNotFound {
            get {
                return ResourceManager.GetString("Errors_SaleIsNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to კონსულტანტი: {0} -ზე იგივე პროდუქტი გაყიდვაში უკვე დამატებულია.
        /// </summary>
        internal static string Errors_SameProductIsAlreadyAddedToConsultant {
            get {
                return ResourceManager.GetString("Errors_SameProductIsAlreadyAddedToConsultant", resourceCulture);
            }
        }
    }
}
