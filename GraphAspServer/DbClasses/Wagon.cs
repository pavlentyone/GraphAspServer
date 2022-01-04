using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Архивная таблица вагонов
    [Table("WAG_ARCH")]
    public class Wagon
    {
        // Идентификатор записи в таблице
        [Column("HID")]
        [ConcurrencyCheck]
        public int Hid { get; set; }

        // id вагона
        [Column("WAG_ID")]
        public int WagonId { get; set; }

        // id поезда (группы вагонов)
        [Column("TRAIN_ID")]
        public int TrainId { get; set; }
        //[ForeignKey("TrainId")]
        //public virtual Train Train { get; set; }

        // id стац.ж.д.объекта
        [Column("RW_ID")]
        public int RailwayId { get; set; }
        //[ForeignKey("RailwayId")]
        //public virtual Railway Railway { get; set; }

        // id операции ->nsi_wag_operations.op_id
        [Column("OP_ID")]
        public int OperationId { get; set; }
        //[ForeignKey("OperationId")]
        //public virtual Operation Operation { get; set; }

        // время операции
        [Column("OP_TIME")]
        public DateTime OperationDate{get;set;}

        // системное время изменения записи
        [Column("OP_SYSTIME")]
        public DateTime OperationChanged { get; set; }

        // номер вагона
        [Column("WAG_NUM"), Required]
        public string WagonNumber { get; set; }

        // id типа вагона ->nsi_wag_types.wagtype_id
        [Column("WAGTYPE_ID")]
        public int? WagonTypeId { get; set; }

        // количество осей
        [Column("WAG_AXES")]
        public int? AxleCount { get; set; }

        // вес вагона (тара), кг
        [Column("WAG_WEIGHT")]
        public int? Weight { get; set; }

        // длина вагона, мм
        [Column("WAG_LENGTH")]
        public int? Length { get; set; }

        // грузоподьемность, кг
        [Column("WAG_MAXW")]
        public int? MaxWeight { get; set; }

        // [srv] Признак блокировки записи (дата)
        [Column("LOCKED")]
        public DateTime? Locked { get; set; }

        // [srv] Имя пользователя, заблокировавшего запись
        [Column("UN_LOCK")]
        public string Locker { get; set; }

        // порядковый номер в поезде/группе
        [Column("WAG_ORDER")]
        public int? OrderNumber { get; set; }

        // id перевозчика > nsi_carriers.carr_id
        [Column("CARR_ID")]
        public int? CarrierId { get; set; }

        // id пути сортировки > rwobjtree.rw_id
        [Column("S_RW_ID")]
        public int? SortWayId { get; set; }

        // id пути перегруза > rwobjtree.rw_id
        [Column("T_RW_ID")]
        public int? OverloadWayId { get; set; }

        // ЕСР станции назначения
        [Column("DEST_ESR")]
        public string DestinationUnifiedMarking { get; set; }

        // код ГНГ груза
        [Column("CARGO_NHM")]
        public string CargoCode { get; set; }

        // код получателя
        [Column("RECEIVER")]
        public string ReceiverCode { get; set; }

        // вес груза по ДУ-1, кг
        [Column("DU1_WGHT")]
        public int? CargoWeightDU1 { get; set; }

        // кол-во контейнеров по ДУ-1 (1зн - груженых, 2зн - порожних)
        [Column("DU1_CNTR")]
        public string ContainerCountDU1 { get; set; }

        // негабаритность по ДУ-1
        [Column("DU1_NG")]
        public int? OversizeDU1 { get; set; }
    }
}
