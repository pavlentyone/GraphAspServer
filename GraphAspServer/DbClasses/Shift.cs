using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Таблица состава смен ст.Брест-Сев. для ГИР
    [Table("GIR_SHIFTS")]
    public class Shift
    {
        // [srv] ID записи в таблице
        [Column("HID")]
        public int Hid { get; set; }

        // дата/время начала смены
        [Column("SHIFT_DATE")]
        public DateTime? ShiftDate { get; set; }

        // ID пользователя1
        [Column("SHIFT_USR1")]
        public int? User1Id { get; set; }
        [ForeignKey("User1Id")]
        public virtual User User1 { get; set; }

        // [srv] Признак блокировки записи (дата)
        [Column("LOCKED")]
        public DateTime? Locked { get; set; }

        // [srv] Имя пользователя, заблокировавшего запись
        [Column("UN_LOCK")]
        public string UnLock { get; set; }

        // ID пользователя2
        [Column("SHIFT_USR2")]
        public int? User2Id { get; set; }
        [ForeignKey("User2Id")]
        public virtual User User2 { get; set; }
    }
}
