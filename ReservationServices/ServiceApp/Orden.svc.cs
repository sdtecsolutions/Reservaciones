﻿using System;
using System.Collections.Generic;
using ReservationServices.BusinessEntities;
using ReservationServices.BusinessRules;

namespace ReservationServices.ServiceApp
{
    public class Order : IOrden
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public List<BETipoDeporte> Listar_TiposDeporte()
        {
            var obr = new BROrden();
            var olst = obr.Listar_TiposDeporte();
            return (olst);
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public List<BETipoCancha> Listar_TiposCancha(int COD_TIPO_DEPO)
        {
            var obr = new BROrden();
            var olst = obr.Listar_TiposCancha(COD_TIPO_DEPO);
            return (olst);
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        public List<BEHorario> Listar_Horarios()
        {
            var obr = new BROrden();
            var olst = obr.Listar_Horarios();
            return (olst);
        }

        /// <summary>
        /// Registrar cliente
        /// </summary>
        public BECliente Registrar_Cliente(BECliente obj)
        {
            try
            {
                var obr = new BROrden();
                obr.Registrar_Cliente(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        public BEOrden Registrar_Orden(BEOrden obj)
        {
            try
            {
                var result = DateTime.Compare(obj.FEC_HORA_RESE, DateTime.Today);
                if (result < 0)
                    throw new ArgumentException("La fecha de reserva debe ser mayor o igual a la actual.");

                if (result == 0)
                {
                    var splhr = obj.HOR_INIC.Split(':');
                    var hr = new TimeSpan(Convert.ToInt32(splhr[0]), Convert.ToInt32(splhr[1]), 0);
                    result = TimeSpan.Compare(hr, DateTime.Now.TimeOfDay);
                    if (result < 0)
                        throw new ArgumentException("El horario seleccionado debe ser mayor a la hora actual.");
                }

                var obr = new BROrden();
                obr.Registrar_Orden(obj);

                // Guardando cola para el envío de mensajes de confirmación al cliente
                var clm = new colaMensajes();
                clm.colaPedidoCorreoAsync(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Listar las reservas creadas por el  cliente
        /// </summary>
        public List<BEOrden> Listar_Reservaciones()
        {
            var obr = new BROrden();
            var olst = obr.Listar_Reservaciones();
            return (olst);
        }

        /// <summary>
        /// Realizar logueo
        /// </summary>
        public int LoginUser(BELogin obj)
        {
            var obr = new BRLogin();
            var isValid = obr.Login(obj);
            return (isValid);
        }

        public List<BEOrden> Listar_MensajesPedidos()
        {
            var clm = new colaMensajes();
            var olst = clm.GetAllPedidos();
            return (olst);
        }
    }
}
