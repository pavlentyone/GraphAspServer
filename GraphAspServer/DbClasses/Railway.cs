using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Дерево железнодорожных объектов
    [Table("RWOBJTREE")]
    public class Railway : IEquatable<Railway>
    {
        // Идентификатор записи в таблице
        [Column("HID")]
        
        public int Hid { get; set; }

        // id объекта
        [Key, Column("RW_ID")]
        public int RailwayId { get; set; }

        // код
        [Column("CODE")]
        public string Code { get; set; }

        // наименование
        [Column("NAME")]
        public string Name { get; set; }

        // состояние 0-удален 1-активен
        [Column("STATUS")]
        public int? Status { get; set; }

        // дата изменения состояния
        [Column("SDATE")]
        public DateTime? StatusChanged { get; set; }

        // id родителя
        [Column("PARENT_ID")]
        public int? ParentId { get; set; }
        // родитель
        [ForeignKey("ParentId")]
        public virtual Railway ParentNode { get; set; }

        // ранг
        [Column("RANK")]
        public int? Rank { get; set; }

        // [srv] Признак блокировки записи (дата)
        [Column("LOCKED")]
        public DateTime? LockedDate { get; set; }

        // [srv] Имя пользователя, заблокировавшего запись
        [Column("UN_LOCK")]
        public string Unlock { get; set; }

        // Какой-то тип. nsi_rw.rw_types
        [Column("TYPE")]
        public int? Type { get; set; }

        // x - координата для формы
        [Column("X")]
        public int? X { get; set; }

        // y - координата для формы
        [Column("Y")]
        public int? Y { get; set; }

        // Условное обозначение УПП
        [Column("UPP_NM")]
        public string ManEntManagement { get; set; }

        public override string ToString()
        {
            var type = typeof(Railway);
            var fields = type.GetFields();

            var sb = new StringBuilder();

            sb.Append("Railway { ");
            foreach(var field in fields)
                sb.Append($"{field.Name} : {field.GetValue(this)}; ");

            var result = sb.ToString();
            return result;
        }

        public bool Equals(Railway value) => this.RailwayId == value?.RailwayId ? true : false;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            var asRailway = obj as Railway;
            if (asRailway is null) return false;
            return Equals(asRailway);
        }

        public override int GetHashCode() => this.RailwayId.GetHashCode();

    }
}