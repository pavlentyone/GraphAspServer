using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Архивная таблица локомотивов
    [Table("LOCO_ARCH")]
    public class Locomotive : IComparable<Locomotive>, IEqualityComparer<Locomotive>, IComparer<Locomotive>
    {
        // Идентификатор записи в таблице
        [Column("HID")]
        public int Hid { get; set; }

        // id локомотива
        [Column("LOCO_ID")]
        public int LocomotiveId { get; set; }

        // id поезда
        [Column("TRAIN_ID")]
        public int? TrainId { get; set; }
        [ForeignKey("TrainId")]
        public virtual Train Train { get; set; }

        // id ж.д.объекта ->rwobjtree.rw_id
        [Column("RW_ID")]
        public int RailwayId { get; set; }
        [ForeignKey("RailwayId"), Required]
        public virtual Railway Railway { get; set; }    

        // id операции ->nsi_loco_operations.op_id
        [Column("OP_ID")]
        public int OperationId { get; set; }
        [ForeignKey("OperationId"), Required]
        public virtual Operation Operation { get; set; }

        // время операции
        [Column("OP_TIME")]
        public DateTime OperationDate { get; set; }

        // системное время изменения записи
        [Column("OP_SYSTIME")]
        public DateTime EditNoteDate { get; set; }

        // номер локомотива
        [Column("LOCO_NUM")]
        public string LocomotiveNumber { get; set; }

        // id серии локомотива ->nsi_locoserie.ls_id
        [Column("LS_ID")]
        public int? LocomotiveSeriesId { get; set; }

        // [srv] Признак блокировки записи (дата)
        [Column("LOCKED")]
        public DateTime? Locked { get; set; }

        // [srv] Имя пользователя, заблокировавшего запись
        [Column("UN_LOCK")]
        public string Unlocked { get; set; }

        // Позиция на пути
        [Column("POS_ON_WAY")]
        public int? WayPosition { get; set; }

        // id перевозчика > nsi_carriers.carr_id
        [Column("CARR_ID")]
        public int? CarrierId { get; set; }
        [ForeignKey("CarrierId")]
        public virtual Carrier Carrier { get; set; }

        // 1-в голове, 2-двойная тяга,3-резервом, 4-одиночное следование
        [Column("STATUS")]
        public int? Status { get; set; }

        public int CompareTo(Locomotive value)
        {
            if (this.LocomotiveId > value.LocomotiveId)
                return 1;
            else if (this.LocomotiveId < value.LocomotiveId)
                return -1;

            if (this.OperationDate > value.OperationDate)
                return 1;
            else if (this.OperationDate < value.OperationDate)
                return -1;

            return 0;
        }

        public int Compare(Locomotive value1, Locomotive value2)
        {
            if (value1.LocomotiveId > value2.LocomotiveId)
                return 1;
            else if (value1.LocomotiveId < value2.LocomotiveId)
                return -1;

            if (value1.OperationDate > value2.OperationDate)
                return 1;
            else if (value1.OperationDate < value2.OperationDate)
                return -1;

            return 0;
        }

        public bool Equals(Locomotive value1, Locomotive value2) => value1.LocomotiveId == value2.LocomotiveId && value1.OperationDate == value2.OperationDate;

        public int GetHashCode(Locomotive value) => value.LocomotiveId.GetHashCode() ^ value.OperationDate.GetHashCode();
    }
}
