using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocsSystem.Model
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Nam = 1,
        [Display(Name = "Nữ")]
        Nu = 2
    }
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Họ và tên")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(50)]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Email is not valid")]
        [Required(ErrorMessage = "Bạn cần nhập tài khoản")]
        public string UserEmail { get; set; }


        [Display(Name = "Chức danh")]
        [Column(TypeName = "nvarchar(100)")]

        public string Title { get; set; }

        [Display(Name = "Giới Tính")]
        public Gender UserGender { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }



        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "Mật khẩu không khớp")]
        [NotMapped]
        public string UserCofirmPassword { get; set; }


        [ForeignKey("roleModel")]
        public int UserRole { get; set; }

        public string UserRoleName { get; set; }
        public bool UserBlock { get; set; }

        public bool UserStatus { get; set; }

        public bool IsDelete { get; set; }
        public RoleModel roleModel { get; set; }
        public int GroupId { get; set; }
        public GroupPermission GroupPermission { get; set; }

        public List<DocumentList> documentLists { get; set; }


    }
}
