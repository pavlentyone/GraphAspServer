using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Представители клиентов
    [Table("USR")]
    public class User
    {
        // [srv] ID записи
        [Column("HID")]
        public int Hid { get; set; }

        // Логин
        [Column("UN")]
        public string Login { get; set; }

        // Пароль
        [Column("PS")]
        public string Password { get; set; }

        // Ф.И.О. пользователя
        [Column("FIO")]
        public string FullName { get; set; }

        // Должность
        [Column("POST")]
        public string Post { get; set; }

        // 
        [Column("LOCKED")]
        public DateTime? Locked { get; set; }

        // 
        [Column("UN_LOCK")]
        public string Unlock { get; set; }

        // 
        [Column("ALTERED")]
        public DateTime? Altered { get; set; }

        // 
        [Column("DATTR")]
        public DateTime? Dattr { get; set; }

        // 1-бан, 0-живой
        [Column("BLOCKED")]
        public int? Blocked { get; set; }

        // Привилегии
        [Column("PRIV")]
        public string Privilege { get; set; }

        // id <-oids.hid
        [Column("USR_ID"), Key]
        public int UserId { get; set; }

        // табельный номер  
        [Column("TN")]
        public string PersonnelNumber { get; set; }

        // фамилия
        [Column("SURNAME")]
        public string Surname { get; set; }

        // имя
        [Column("NAME")]
        public string Name { get; set; }

        // отчество
        [Column("PATRONYMIC")]
        public string Patronymic { get; set; }
    }
}
