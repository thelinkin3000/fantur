using System;

namespace FantasticTour.Models.ViewModels
{
    public class EmailConfirmVm
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class HotelVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CiudadId { get; set; }
    }

    public class TransporteVm
    {
        public int Id { get; set; }
        public DateTime FechaIda { get; set; }
        public DateTime FechaVuelta { get; set; }
        public decimal Costo { get; set; }
        public int OrigenId { get; set; }
        public string Origen { get; set; }
        public int DestinoId { get; set; }
        public string Destino { get; set; }
        public string TipoTransporte { get; set; }
        public int TipoTransporteId { get; set; }
    }

    public class MailingVm
    {
        public string Titulo { get; set; }  
        public string Cuerpo { get; set; }  
    }
    public class AtraccionVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; } 
        public Decimal Costo { get; set; }
        public int CiudadId { get; set; }
        public string Ciudad { get; set; }
    }

    public class EstadiaVm
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Decimal Costo { get; set; }
        public int HotelId { get; set; }
        public string Hotel { get; set; }
        public string Ciudad { get; set; }
    }

    public class PaqueteVm
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Disponible { get; set; }
        public int TransporteId { get; set; }
        public int EstadiaId { get; set; }
        public int AtraccionId { get; set; }
        public string Atraccion{ get; set; }
        public string Hotel { get; set; }
        public decimal Costo { get; set; }
    }
    
    public class CrearPaqueteVm
    {
        public String Nombre { get; set; }
        public DateTime FechaVencimiento { get; set; }
        //Transporte
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public DateTime Partida { get; set; }
        public DateTime Regreso { get; set; }
        public decimal CostoTransporte { get; set; }
        //Estadia
        public int HotelId { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime Egreso { get; set; }
        public Decimal CostoEstadia { get; set; }
        public int AtraccionId { get; set; }
        
    }
}