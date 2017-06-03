using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IBLayer
    {
        int Login(Login usr);

        void AddSensor(Sensores sen);
        void DeleteSensor(int id);
        void UpdateSensor(Sensores sen);
        List<Sensores> GetAllSensores();
        Sensores GetSensor(int id);

        void AddTipo(TipoSensor sen);
        void DeleteTipo(int id);
        void UpdateTipo(TipoSensor sen);
        List<TipoSensor> GetAllTipos();
        TipoSensor GetTipo(int id);

        void AddValor(Valores val);
        void DeleteValor(int id);
        void UpdateValor(Valores val);
        List<Valores> GetAllValores();
        Valores GetValor(int id);
        List<Valores> GetValoresDeSensor(int idSensor);
        List<Valores> GetValoresDeSensorConFecha(int idSensor, String fecha);

        void AddEvento(Evento ev);
        void AddEventoComplejo(EventoComplejo ev);
        void DeleteEvento(int id);
        void UpdateEvento(Evento ev);
        List<Evento> GetAllEventos();
        Evento GetEvento(int id);

        void AddUsuario(Usuario ev);
        void DeleteUsuario(int id);
        void UpdateUsuario(Usuario ev);
        List<Usuario> GetAllUsuarios();
        Usuario GetUsuario(int id);

        void AddSubscripcion(EventoSubscripcion sub);
        void DeleteSubscripcion(int id);
        void UpdateSubscripcion(EventoSubscripcion sub);
        List<EventoSubscripcion> GetAllSubscripciones();
        EventoSubscripcion GetSubscripcion(int id);

        void AddRelacionEvento(EventoRelacion sub);
        void DeleteRelacionEvento(int idev1, int idev2);
        void UpdateRelacionEvento(EventoRelacion sub);
        List<EventoRelacion> GetAllRelacionesEventos();
    }
}
