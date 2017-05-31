﻿using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDALayer
    {
        void AddTipo(TipoSensor sen);
        void DeleteTipo(int id);
        void UpdateTipo(TipoSensor sen);
        List<TipoSensor> GetAllTipos();
        TipoSensor GetTipo(int id);

        void AddSensor(Sensores sens);
        void DeleteSensor(int id);
        void UpdateSensor(Sensores sens);
        List<Sensores> GetAllSensores();
        Sensores GetSensor(int id);

        void AddValor(Valores val);
        void DeleteValor(int id);
        void UpdateValor(Valores val);
        List<Valores> GetAllValores();
        Valores GetValor(int id);
        List<Valores> GetValoresDeSensor(int idSensor);
        List<Valores> GetValoresDeSensorConFecha(int idSensor, String fecha);

        void AddEvento(Evento ev);
        void DeleteEvento(int id);
        void UpdateEvento(Evento ev);
        List<Evento> GetAllEventos();
        Evento GetEvento(int id);

        void AddZona(Zona ev);
        void DeleteZona(int id);
        void UpdateZona(Zona ev);
        List<Zona> GetAllZonas();
        Zona GetZona(int id);

        void AddUsuario(Usuario ev);
        void DeleteUsuario(int id);
        void UpdateUsuario(Usuario ev);
        List<Usuario> GetAllUsuarios();
        Usuario GetUsuario(int id);

        void AddSubscripcion(Subscripcion sub);
        void DeleteSubscripcion(int id);
        void UpdateSubscripcion(Subscripcion sub);
        List<Subscripcion> GetAllSubscripciones();
        Subscripcion GetSubscripcion(int id);
    }
}
