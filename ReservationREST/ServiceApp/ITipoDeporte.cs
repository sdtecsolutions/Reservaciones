﻿using System.Collections.Generic;
using System.ServiceModel;
using ReservationREST.BusinessEntities;
using System.ServiceModel.Web;

namespace ReservationREST.ServiceApp
{
    [ServiceContract]
    public interface ITipoDeporte
    {
        /// <summary>
        /// Lista los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "TipoDeporte", ResponseFormat = WebMessageFormat.Json)]
        List<BETipoDeporte> Listar_TiposDeporte();

        /// <summary>
        /// Registrar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "TipoDeporte", ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte Registrar_TipoDeporte(BETipoDeporte obj);

        /// <summary>
        /// Actualizar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "TipoDeporte", ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte Actualizar_TipoDeporte(BETipoDeporte obj);

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "TipoDeporte/{COD_TIPO_DEPO}", ResponseFormat = WebMessageFormat.Json)]
        List<BETipoDeporte> Obtener_TipoDeporte(string COD_TIPO_DEPO);

        /// <summary>
        /// Eliminar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "TipoDeporte/{COD_TIPO_DEPO}", ResponseFormat = WebMessageFormat.Json)]
        void Eliminar_TipoDeporte(string COD_TIPO_DEPO);

    }
}
