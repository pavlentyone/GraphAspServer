using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbClasses
{
    // Справочник "Перевозчики"
    [Table("NSI_CARRIERS")]
    public class Carrier
    {
        // 
        [Column("HID")]
        public int Hid { get; set; }

        // Id перевозчика
        [Column("CARR_ID")]
        public int CarrierId { get; set; }

        // Код перевозчика
        [Column("CARR_CODE")]
        public string CarrierCode { get; set; }

        // Наименование перевозчика
        [Column("CARR_NAME")]
        public string CarrierName { get; set; }

        // Краткое наименование перевозчика
        [Column("CARR_SNAME")]
        public string ShortCarrierName { get; set; }

        // статус 1=живой, 0=дохлый
        [Column("STATUS")]
        public int? Status { get; set; }

        // дата изменения статуса
        [Column("SDATE")]
        public DateTime? StatusChanged { get; set; }
        
        // 
        [Column("LOCKED")]
        public DateTime? Locked { get; set; }
        
        // 
        [Column("UN_LOCK")]
        public string Unlock { get; set; }
        
        // мнемокод перевозчика
        [Column("CARR_MNEMO")]
        public string Mnemocode { get; set; }

        // id клиента (если перевозчик является клиентом)
        [Column("KLIENT_ID")]
        public int? ClientId { get; set; }
    }
}
