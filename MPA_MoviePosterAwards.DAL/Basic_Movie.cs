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
    
    public partial class Basic_Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Basic_Movie()
        {
            this.Step_Celeb_Movie = new HashSet<Step_Celeb_Movie>();
            this.Step_Movie_Country = new HashSet<Step_Movie_Country>();
            this.Step_Movie_Genre = new HashSet<Step_Movie_Genre>();
            this.Step_Movie_Lang = new HashSet<Step_Movie_Lang>();
            this.Step_Movie_Rating = new HashSet<Step_Movie_Rating>();
            this.Step_Movie_Poster = new HashSet<Step_Movie_Poster>();
            this.Basic_Poster = new HashSet<Basic_Poster>();
        }
    
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Title_En { get; set; }
        public string Aka { get; set; }
        public Nullable<short> Year { get; set; }
        public string Website { get; set; }
        public Nullable<short> Current_Season { get; set; }
        public Nullable<short> Season_Count { get; set; }
        public Nullable<int> Episode_Count { get; set; }
        public string Pubdate { get; set; }
        public string Duration { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Celeb_Movie> Step_Celeb_Movie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Movie_Country> Step_Movie_Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Movie_Genre> Step_Movie_Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Movie_Lang> Step_Movie_Lang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Movie_Rating> Step_Movie_Rating { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step_Movie_Poster> Step_Movie_Poster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basic_Poster> Basic_Poster { get; set; }
    }
}
