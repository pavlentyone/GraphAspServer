using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Архивная таблица поездов
    [Table("TRAIN_ARCH")]
    public class Train : IComparable<Train>, IEqualityComparer<Train>, IComparer<Train>
    {
        // Идентификатор записи в таблице
        [Column("HID")]
        public int Hid { get; set; }

        // id поезда
        [Column("TRAIN_ID")]
        public int TrainId { get; set; }

        // id стационарного ж.д.объекта ->rwobjtree.rw_id
        [Column("RW_ID")]
        public int RailwayId { get; set; }
        [ForeignKey("RailwayId"), Required]
        public virtual Railway Railway { get; set; }

        // id операции ->nsi_train_operations.op_id
        [Column("OP_ID")]
        public int OperationId { get; set; }
        [ForeignKey("OperationId"), Required]
        public virtual Operation Operation { get; set; }

        // id объекта определяющего направление ->rwrouters.route_id
        [Column("ROUTE_ID")]
        public int? DirectionId { get; set; }

        // id перевозчика ->nsi_carriers.carr_id
        [Column("CARR_ID")]
        public int? CarrierId { get; set; }
        [ForeignKey("CarrierId")]
        public virtual Carrier Carrier { get; set; }

        // номер поезда
        [Column("TRAIN_NUM")]
        public string TrainNumber { get; set; }

        // 1-я часть индекса поезда
        [Column("INDEX1")]
        public string FirstIndexPart { get; set; }

        // 2-я часть (середина) индекса поезда
        [Column("INDEX2")]
        public string SecondIndexPart { get; set; }

        // 3-я часть индекса поезда
        [Column("INDEX3")]
        public string ThirdIndexPart { get; set; }

        // время операции
        [Column("OP_TIME")]
        public DateTime OperationDate { get; set; }

        // системное время изменения записи
        [Column("OP_SYSTIME")]
        public DateTime EditNoteDate { get; set; }

        // [srv] Признак блокировки записи (дата)
        [Column("LOCKED")]
        public DateTime? LockNoteDate { get; set; }

        // [srv] Имя пользователя, заблокировавшего запись
        [Column("UN_LOCK")]
        public string LockerName { get; set; }

        // позиция на пути
        [Column("POS_ON_WAY")]
        public int? WayPosition { get; set; }

        // Масса поезда брутто (кг)
        [Column("T_BRUTTO")]
        public int? BruttoWeight { get; set; }

        // Длина поезда (мм)
        [Column("T_LENGTH")]
        public int? TrainLength { get; set; }

        // Код прикрытия
        [Column("COVERCODE")]
        public string CoverCode { get; set; }

        // Индекс негабаритности
        [Column("OVERSIZE")]
        public string OversizeIndex { get; set; }

        // ID поезда-родителя (для объединенных поездов)
        [Column("PARENT_ID")]
        public int? ParentId { get; set; }

        public Wagon[] wagons { get; set; }

        public Locomotive[] locomotives { get; set; }

        public Train()
        {
            //wagons = new Wagon[0];
            //locomotives = new Locomotive[0];
        }

        public int CompareTo(Train value)
        {
            if (this.TrainId > value.TrainId)
                return 1;
            else if (this.TrainId < value.TrainId)
                return -1;

            if (this.OperationDate > value.OperationDate)
                return 1;
            else if (this.OperationDate < value.OperationDate)
                return -1;

            return 0;
        }

        public int Compare(Train value1, Train value2)
        {
            if (value1.TrainId > value2.TrainId)
                return 1;
            else if (value1.TrainId < value2.TrainId)
                return -1;

            if (value1.OperationDate > value2.OperationDate)
                return 1;
            else if (value1.OperationDate < value2.OperationDate)
                return -1;

            return 0;
        }

        public bool Equals(Train value) => this.TrainId == value.TrainId && this.OperationDate == value.OperationDate;
        public bool Equals(Train value1, Train value2) => value1.TrainId == value2.TrainId && value1.OperationDate == value2.OperationDate;

        public int GetHashCode(Train value) => value.TrainId.GetHashCode() ^ value.OperationDate.GetHashCode();
    }
}
