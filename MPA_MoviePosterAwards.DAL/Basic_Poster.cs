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
    
    public partial class Basic_Poster
    {
        public System.Guid Id { get; set; }
        public System.Guid Movie { get; set; }
        public System.Guid User { get; set; }
        public string Poster { get; set; }
        public string Poster_S { get; set; }
        public string Poster_XS { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
    
        public virtual Basic_Movie Basic_Movie { get; set; }
        public virtual Basic_User Basic_User { get; set; }
    }
}
