using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALayer : IDALayer
    {
        private int maxID()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                int max = db_context.Evento.Max(e => e.EventoId);
                return max;
            }
        }

        public void AddTipo(TipoSensor tipo)
        {
            Model.Tipo_Sensor tipo_sensor;
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                tipo_sensor = new Model.Tipo_Sensor()
                {
                    Nombre = tipo.Nombre,
                    Frecuencia = tipo.Frecuencia,
                    TipoDato = tipo.TipoDato
                };
                db_context.Tipo_Sensor.Add(tipo_sensor);
                db_context.SaveChanges();
            }
        }

        public void DeleteTipo(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var tipo = (from t in db_context.Tipo_Sensor
                            where t.Id == id
                            select t).First();
                db_context.Tipo_Sensor.Remove(tipo);
                db_context.SaveChanges();
            }
        }

        public void UpdateTipo(TipoSensor tipo)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Tipo_Sensor tip = db_context.Tipo_Sensor.Find(tipo.Id);
                tip.Nombre = tipo.Nombre;
                tip.Frecuencia = tipo.Frecuencia;
                tip.TipoDato = tipo.TipoDato;

                db_context.SaveChanges();
            }
        }

        public List<TipoSensor> GetAllTipos()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<TipoSensor> tipos_sensores = new List<TipoSensor>();
                var tipos_db = from t in db_context.Tipo_Sensor
                                  select t;

                foreach (Model.Tipo_Sensor tipo in tipos_db)
                {
                    TipoSensor tipo_sensor = new TipoSensor()
                    {
                        Id = tipo.Id,
                        Nombre = tipo.Nombre,
                        Frecuencia = tipo.Frecuencia,
                        TipoDato = tipo.TipoDato
                    };
                    tipos_sensores.Add(tipo_sensor);
                    
                }

                return tipos_sensores;
            }
        }

        public TipoSensor GetTipo(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var tipo_db = (from t in db_context.Tipo_Sensor
                              where t.Id == id
                              select t).First();
                TipoSensor ts = new TipoSensor()
                {
                    Id = tipo_db.Id,
                    Nombre = tipo_db.Nombre,
                    Frecuencia = tipo_db.Frecuencia,
                    TipoDato = tipo_db.TipoDato
                };
                return ts;
            }
        }

        public void AddSensor(Sensores sens)
        {
            Model.Sensores _sen;
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                _sen = new Model.Sensores()
                {
                    //Id = sens.Id,
                    Tipo = sens.Tipo,
                    Descripcion = sens.Descripcion,
                    Latitud = sens.Latitud,
                    Longitud = sens.Longitud
                };
                db_context.Sensores.Add(_sen);
                db_context.SaveChanges();
            }
        }

        public void DeleteSensor(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var sens = (from s in db_context.Sensores
                            where s.Id == id
                            select s).First();
                db_context.Sensores.Remove(sens);
                db_context.SaveChanges();
            }
        }

        public void UpdateSensor(Sensores sens)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Sensores sensor = db_context.Sensores.Find(sens.Id);

                sensor.Tipo = sens.Tipo;
                sensor.Latitud = sens.Latitud;
                sensor.Longitud = sens.Longitud;
                sensor.Descripcion = sens.Descripcion;
                
                db_context.SaveChanges();
            }
        }

        public List<Sensores> GetAllSensores()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Sensores> sensores = new List<Sensores>();
                var sensores_db = from s in db_context.Sensores
                                  select s;

                foreach (Model.Sensores sen in sensores_db)
                {
                    Sensores s = new Sensores()
                    {
                        Id = sen.Id,
                        Tipo = sen.Tipo,
                        Descripcion = sen.Descripcion,
                        Latitud = (float)sen.Latitud,
                        Longitud = (float)sen.Longitud
                    };
                    sensores.Add(s);
                    Console.WriteLine(s);
                }

                return sensores;
            }
        }

        public Sensores GetSensor(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var sensor = (from s in db_context.Sensores
                             where s.Id == id
                             select s).First();
                
                return new Sensores()
                {
                    Id = sensor.Id,
                    Tipo = sensor.Tipo,
                    Descripcion = sensor.Descripcion,
                    Latitud = (float)sensor.Latitud,
                    Longitud = (float)sensor.Longitud
                };
            }
        }

        public void AddValor(Valores val)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Valores v = new Model.Valores()
                {
                    SensorId = val.Id_Sensor,
                    Valor = val.Valor_,
                    Fecha = val.Fecha
                };
                db_context.Valores.Add(v);
                db_context.SaveChanges();
            }
        }

        public void DeleteValor(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Valores v = db_context.Valores.Find(id);
                db_context.Valores.Remove(v);
                db_context.SaveChanges();
            }
        }

        public void UpdateValor(Valores val)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Valores v = db_context.Valores.Find(val.Id);
                v.Valor = val.Valor_;
                v.Fecha = val.Fecha;
                db_context.SaveChanges();
            }
        }

        public List<Valores> GetAllValores()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Valores> valores = new List<Valores>();
                var valores_db = from v in db_context.Valores
                                 select v;
                foreach (Model.Valores val in valores_db)
                {
                    Valores v = new Valores()
                    {
                        Id = val.Id,
                        Id_Sensor = val.SensorId,
                        Valor_ = val.Valor,
                        Fecha = val.Fecha
                    };

                    valores.Add(v);
                }

                return valores;
            }
        }

        public Valores GetValor(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var valor = (from v in db_context.Valores
                             where v.Id == id
                             select v).First();

                return new Valores()
                {
                    Id = valor.Id,
                    Id_Sensor = valor.SensorId,
                    Valor_ = valor.Valor,
                    Fecha = valor.Fecha
                };
            }
        }

        public List<Valores> GetValoresDeSensor(int idSensor)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Valores> valores = new List<Valores>();
                var valor = (from v in db_context.Valores
                             where v.SensorId == idSensor
                             select v);
                foreach (Model.Valores v in valor)
                {
                    Valores val = new Valores()
                    {
                        Id = v.Id,
                        Id_Sensor = v.SensorId,
                        Valor_ = v.Valor,
                        Fecha = v.Fecha
                    };

                    valores.Add(val);
                }
                return valores;
            }

        }

        public List<Valores> GetValoresDeSensorConFecha(int idSensor, String fecha)
        {
            DateTime dt = Convert.ToDateTime(fecha);
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Valores> valores = new List<Valores>();
                var valor = (from v in db_context.Valores
                             where v.SensorId == idSensor
                             where v.Fecha == dt
                             select v);
                foreach (Model.Valores v in valor)
                {
                    Valores val = new Valores()
                    {
                        Id = v.Id,
                        Id_Sensor = v.SensorId,
                        Valor_ = v.Valor,
                        Fecha = v.Fecha
                    };

                    valores.Add(val);
                }
                return valores;
            }

        }

        public void AddEvento(Evento ev)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Evento evento = new Model.Evento()
                {
                    Nombre = ev.Nombre,
                    ValorLimite = ev.ValorLimite,
                    Operador = ev.Operador,
                    TipoDato = ev.TipoDato
                };

                db_context.Evento.Add(evento);
                db_context.SaveChanges();
            }
        }

        public void AddEventoComplejo(EventoComplejo ev)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Evento evento = new Model.Evento()
                {
                    Nombre = ev.Nombre,
                    Operador = ev.Operador,
                    //ChannelName = "channel",
                    //ValorLimite = "20",
                    //TipoDato = "tipodato"
                };
                db_context.Evento.Add(evento);

                foreach (Evento even in ev.Hijos) {
                    Model.EventoRelacion relev = new Model.EventoRelacion()
                    {
                        //Tengo que obtener el nro de id del padre (el que agrego arriba)
                        EvPadreId = maxID()+1,
                        EvHijoId = even.EventoId,
                        Operador = ev.Operador,
                        Activado = false
                    };
                    db_context.EventoRelacion.Add(relev);
                }

                db_context.SaveChanges();
            }
        }

        public void DeleteEvento(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var ev = (from e in db_context.Evento
                          where e.EventoId == id
                          select e).First();
                db_context.Evento.Remove(ev);
                db_context.SaveChanges();
            }
        }

        public void UpdateEvento(Evento ev)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Evento evento = db_context.Evento.Find(ev.EventoId);
                evento.Nombre = ev.Nombre;
                evento.ValorLimite = ev.ValorLimite;
                evento.Operador = ev.Operador;
                evento.TipoDato = ev.TipoDato;

                db_context.SaveChanges();
            }
        }

        public List<Evento> GetAllEventos()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Evento> eventos = new List<Evento>();
                var eventos_db = from e in db_context.Evento
                                 select e;

                foreach (Model.Evento ev in eventos_db)
                {
                    Evento e = new Evento()
                    {
                        EventoId = ev.EventoId,
                        Nombre = ev.Nombre,
                        ValorLimite = ev.ValorLimite,
                        Operador = ev.Operador,
                        TipoDato = ev.TipoDato
                    };

                    eventos.Add(e);
                };

                return eventos;
            }
        }

        public Evento GetEvento(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var ev = (from e in db_context.Evento
                          where e.EventoId == id
                          select e).First();

                Evento evento = new Evento()
                {
                    EventoId = ev.EventoId,
                    Nombre = ev.Nombre,
                    ValorLimite = ev.ValorLimite,
                    Operador = ev.Operador,
                    TipoDato = ev.TipoDato
                };

                return evento;
            }
        }

        public void AddUsuario(Usuario u)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Usuario usr = new Model.Usuario()
                {
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    TokenId = u.TokenId,
                    ChannelName = u.ChannelName
                };

                db_context.Usuario.Add(usr);
                db_context.SaveChanges();
            }
        }

        public void DeleteUsuario(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var usr = (from u in db_context.Usuario
                            where u.UsuarioId == id
                            select u).First();
                db_context.Usuario.Remove(usr);
                db_context.SaveChanges();
            }
        }

        public void UpdateUsuario(Usuario u)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.Usuario usr = db_context.Usuario.Find(u.UsuarioId);
                usr.Nombre = u.Nombre;
                usr.Apellido = u.Apellido;
                usr.TokenId = u.TokenId;

                db_context.SaveChanges();
            }
        }

        public List<Usuario> GetAllUsuarios()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<Usuario> usuarios = new List<Usuario>();
                var usrs_db = from u in db_context.Usuario
                               select u;

                foreach (Model.Usuario usr in usrs_db)
                {
                    Usuario z = new Usuario()
                    {
                        UsuarioId = usr.UsuarioId,
                        Nombre = usr.Nombre,
                        Apellido = usr.Apellido,
                        TokenId = usr.TokenId,
                        ChannelName = usr.ChannelName
                    };

                    usuarios.Add(z);
                };

                return usuarios;
            }
        }

        public Usuario GetUsuario(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var us = (from u in db_context.Usuario
                          where u.UsuarioId == id
                          select u).First();

                Usuario zona = new Usuario()
                {
                    UsuarioId = us.UsuarioId,
                    Nombre = us.Nombre,
                    Apellido = us.Apellido,
                    TokenId = us.TokenId,
                    ChannelName = us.ChannelName
                };

                return zona;
            }
        }

        public void AddSubscripcion(EventoSubscripcion u)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.EventoSubscripcion ev_sub = new Model.EventoSubscripcion()
                {
                    EventoId = u.EventoId,
                    UsuarioId = u.UsuarioId,
                    Radio = u.Radio,
                    CentroLatitud = u.CentroLatitud,
                    CentroLongitud = u.CentroLongitud
                };

                db_context.EventoSubscripcion.Add(ev_sub);
                db_context.SaveChanges();
            }
        }

        public void DeleteSubscripcion(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var sub = (from s in db_context.EventoSubscripcion
                           where s.Id == id
                           select s).First();
                db_context.EventoSubscripcion.Remove(sub);
                db_context.SaveChanges();
            }
        }

        public void UpdateSubscripcion(EventoSubscripcion u)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.EventoSubscripcion sub = db_context.EventoSubscripcion.Find(u.Id);
                sub.UsuarioId = u.UsuarioId;
                sub.EventoId = u.EventoId;
                sub.Radio = u.Radio;
                sub.CentroLongitud = u.CentroLongitud;
                sub.CentroLatitud = u.CentroLatitud;

                db_context.SaveChanges();
            }
        }

        public List<EventoSubscripcion> GetAllSubscripciones()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<EventoSubscripcion> subscripciones = new List<EventoSubscripcion>();
                var subs_db = from s in db_context.EventoSubscripcion
                              select s;

                foreach (Model.EventoSubscripcion subs in subs_db)
                {
                    EventoSubscripcion subscripcion = new EventoSubscripcion()
                    {
                        Id = subs.Id,
                        UsuarioId = subs.UsuarioId,
                        EventoId = subs.EventoId,
                        Radio = subs.Radio,
                        CentroLatitud = subs.CentroLatitud,
                        CentroLongitud = subs.CentroLongitud
                    };

                    subscripciones.Add(subscripcion);
                };

                return subscripciones;
            }
        }

        public EventoSubscripcion GetSubscripcion(int id)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var subs = (from s in db_context.EventoSubscripcion
                          where s.Id == id
                          select s).First();

                EventoSubscripcion subscripcion = new EventoSubscripcion()
                {
                    Id = subs.Id,
                    UsuarioId = subs.UsuarioId,
                    EventoId = subs.EventoId,
                    Radio = subs.Radio,
                    CentroLongitud = subs.CentroLongitud,
                    CentroLatitud = subs.CentroLatitud
                };

                return subscripcion;
            }
        }

        public void AddRelacionEvento(EventoRelacion sub)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.EventoRelacion ev_rel = new Model.EventoRelacion()
                {
                    EvPadreId = sub.EvPadreId,
                    EvHijoId = sub.EvHijoId,
                    Activado = sub.Activado,
                    Operador = sub.Operador
                };

                db_context.EventoRelacion.Add(ev_rel);
                db_context.SaveChanges();
            }
        }

        public void DeleteRelacionEvento(int idev1, int idev2)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                var rel = (from e in db_context.EventoRelacion
                           where e.EvPadreId == idev1 && e.EvHijoId == idev2
                           select e).First();
                db_context.EventoRelacion.Remove(rel);
                db_context.SaveChanges();
            }
        }

        public void UpdateRelacionEvento(EventoRelacion evrel)
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                Model.EventoRelacion rel = db_context.EventoRelacion.Find(evrel.EvPadreId,evrel.EvHijoId);
                rel.Activado = evrel.Activado;
                rel.Operador = evrel.Operador;

                db_context.SaveChanges();
            }
        }

        public List<EventoRelacion> GetAllRelacionesEventos()
        {
            using (Model.DBTSI1Entities db_context = new Model.DBTSI1Entities())
            {
                List<EventoRelacion> subscripciones = new List<EventoRelacion>();
                var relev_db = from r in db_context.EventoRelacion
                              select r;

                foreach (Model.EventoRelacion evrel in relev_db)
                {
                    EventoRelacion relacion_ev = new EventoRelacion()
                    {
                        EvPadreId = evrel.EvPadreId,
                        EvHijoId = evrel.EvHijoId,
                        Activado = evrel.Activado,
                        Operador = evrel.Operador
                    };

                    subscripciones.Add(relacion_ev);
                };

                return subscripciones;
            }
        }
    }
}
