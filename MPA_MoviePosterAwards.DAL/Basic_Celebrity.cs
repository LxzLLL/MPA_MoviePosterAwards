//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MPA_MoviePosterAwards.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Basic_Celebrity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Basic_Celebrity()
        {
            this.Step_Celeb_Movie = new HashSet<Step_Celeb_Movie>();
            this.Step_Celeb_Avatar = new HashSet<Step_Celeb_Avatar>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Aka { get; set; }
        public string Name_En { get; set; }
        public string Aka_En { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Profession { get; set; }
        public string Birth_Date { get; set; }
        public string Death_Date { get; set; }
        public string Born_Place { get; set; }
        public string Family { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Celeb_Movie> Step_Celeb_Movie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Celeb_Avatar> Step_Celeb_Avatar { get; set; }
    }
}
