using DataAccessLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLayer : IBLayer
    {
        private IDALayer _dal;

        public BLayer(IDALayer dal)
        {
            _dal = dal;
        }

        public int Login(Login usr)
        {
            return _dal.Login(usr);
        }

        public void AddTipo(TipoSensor sen)
        {
            _dal.AddTipo(sen);
        }

        public void DeleteTipo(int id)
        {
            _dal.DeleteTipo(id);
        }

        public void UpdateTipo(TipoSensor sen)
        {
            _dal.UpdateTipo(sen);
        }

        public List<TipoSensor> GetAllTipos()
        {
            return _dal.GetAllTipos();
        }

        public TipoSensor GetTipo(int id)
        {
            return _dal.GetTipo(id);
        }

        public void AddSensor(Sensores sen)
        {
            _dal.AddSensor(sen);
        }

        public void DeleteSensor(int id)
        {
            _dal.DeleteSensor(id);
        }

        public void UpdateSensor(Sensores sen)
        {
            _dal.UpdateSensor(sen);
        }

        public List<Sensores> GetAllSensores()
        {
            return _dal.GetAllSensores();
        }

        public Sensores GetSensor(int id)
        {
            return _dal.GetSensor(id);
        }

        public void AddValor(Valores val)
        {
            _dal.AddValor(val);
        }

        public void DeleteValor(int id)
        {
            _dal.DeleteValor(id);
        }

        public void UpdateValor(Valores val)
        {
            _dal.UpdateValor(val);
        }

        public List<Valores> GetAllValores()
        {
            return _dal.GetAllValores();
        }

        public Valores GetValor(int id)
        {
            return _dal.GetValor(id);
        }

        public List<Valores> GetValoresDeSensor(int idSensor)
        {
            return _dal.GetValoresDeSensor(idSensor);
        }

        public List<Valores> GetValoresDeSensorConFecha(int idSensor, String fecha)
        {
            return _dal.GetValoresDeSensorConFecha(idSensor, fecha);
        }

        public void AddEvento(Evento ev)
        {
            _dal.AddEvento(ev);
        }

        public void AddEventoComplejo(EventoComplejo ev)
        {
            _dal.AddEventoComplejo(ev);
        }

        public void DeleteEvento(int id)
        {
            _dal.DeleteEvento(id);
        }

        public void UpdateEvento(Evento ev)
        {
            _dal.UpdateEvento(ev);
        }

        public List<Evento> GetAllEventos()
        {
            return _dal.GetAllEventos();
        }

        public Evento GetEvento(int id)
        {
            return _dal.GetEvento(id);
        }

        public void AddUsuario(Usuario usr)
        {
            _dal.AddUsuario(usr);
        }

        public void DeleteUsuario(int id)
        {
            _dal.DeleteUsuario(id);
        }

        public void UpdateUsuario(Usuario usr)
        {
            _dal.UpdateUsuario(usr);
        }

        public List<Usuario> GetAllUsuarios()
        {
            return _dal.GetAllUsuarios();
        }

        public Usuario GetUsuario(int id)
        {
            return _dal.GetUsuario(id);
        }

        public void AddSubscripcion(EventoSubscripcion sub)
        {
            _dal.AddSubscripcion(sub);
        }

        public void DeleteSubscripcion(int id)
        {
            _dal.DeleteSubscripcion(id);
        }

        public void UpdateSubscripcion(EventoSubscripcion sub)
        {
            _dal.UpdateSubscripcion(sub);
        }

        public List<EventoSubscripcion> GetAllSubscripciones()
        {
            return _dal.GetAllSubscripciones();
        }

        public EventoSubscripcion GetSubscripcion(int id)
        {
            return _dal.GetSubscripcion(id);
        }

        public void AddRelacionEvento(EventoRelacion rel)
        {
            _dal.AddRelacionEvento(rel);
        }

        public void DeleteRelacionEvento(int idev1,int idev2)
        {
            _dal.DeleteRelacionEvento(idev1,idev2);
        }

        public void UpdateRelacionEvento(EventoRelacion rel)
        {
            _dal.UpdateRelacionEvento(rel);
        }

        public List<EventoRelacion> GetAllRelacionesEventos()
        {
            return _dal.GetAllRelacionesEventos();
        }

    }
}
