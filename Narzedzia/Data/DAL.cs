using Microsoft.EntityFrameworkCore;
using Narzedzia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Narzedzia.Data
{
    public interface IDAL
    {

    public List<Events> GetEvents();
    public List<Events> GetMyEvents();

    public Events GetEvent(int id);
    public void CreateEvent(IFormCollection form);
    public void UpdateEvent(IFormCollection form);
    public void DeleteEvent(int id);

    public List<Wydzial> GetWydzials();
    public Wydzial GetWydzial(int id);
    public List<Stanowisko> GetStanowiskos();
    public Stanowisko GetStanowisko(int id);

    public List<Narzedzie> GetNarzedzias();
    public Narzedzie GetNarzedzia(int id);


    }
    public class DAL : IDAL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        public List<Events> GetEvents()
        {
            return db.Events.Include(e => e.Wydzial).Include(e => e.Narzedzie).Include(e => e.Stanowisko).ToList();
        }
        public List<Events> GetMyEvents()
        {
            return db.Events.ToList();
        }

       
        public Events GetEvent(int id)
        {
            /*            return db.Events.FirstOrDefault(x => x.IdCal == id);
            */
            return db.Events
                 .Include(e => e.Stanowisko)
                 .Include(e => e.Wydzial)
                 .Include(e => e.Narzedzie)
                 .Include(e => e.Narzedzie.Uzytkownicy)
                 .FirstOrDefault(x => x.IdCal == id);
        }
        public void CreateEvent(IFormCollection form)
        {
            
                var newEvent = new Events();

                newEvent.NameCal = form["Events.NameCal"].ToString();
                newEvent.DescriptionCal = form["Events.DescriptionCal"].ToString();
                newEvent.StartCal = DateTime.Parse(form["Events.StartCal"].ToString());
                newEvent.EndCal = DateTime.Parse(form["Events.EndCal"].ToString());

                var narzedziename = form["Narzedzia"].ToString();
                var stanowiskaname = form["Stanowiska"].ToString();
                var wydzialyname = form["Wydzialy"].ToString();

                newEvent.NarzedzieId = db.Narzedzia.FirstOrDefault(x => x.Nazwa == narzedziename)?.NarzedzieId;
                newEvent.StanowiskoId = db.Stanowiska.FirstOrDefault(x => x.NazwaStanowiska == stanowiskaname)?.StanowiskoId;
                newEvent.WydzialId = db.Wydzialy.FirstOrDefault(x => x.NazwaWydzialu == wydzialyname)?.WydzialId;

                db.Events.Add(newEvent);
                db.SaveChanges();

            
           
        }

        public void UpdateEvent(IFormCollection form)
        {
            var narzedziename = form["Narzedzia"].ToString();
            var stanowiskaname = form["Stanowiska"].ToString();
            var wydzialyname = form["Wydzialy"].ToString();
            var eventid = int.Parse(form["Events.IdCal"]);
           // var eventid = form["IdCal"].ToString();
            var myevent = db.Events.FirstOrDefault(x => x.IdCal == eventid );
            var narzedzia = db.Narzedzia.FirstOrDefault(x => x.Nazwa == narzedziename);
            var stanowiska = db.Stanowiska.FirstOrDefault(x => x.NazwaStanowiska == stanowiskaname);
            var wydzialy = db.Wydzialy.FirstOrDefault(x => x.NazwaWydzialu == wydzialyname);
            myevent.UpdateEvent(form, narzedzia, stanowiska, wydzialy);
            db.Entry(myevent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteEvent(int id)
        {
            var myevent = db.Events.Find(id);
            db.Events.Remove(myevent);
            db.SaveChanges();
        }

        public List<Wydzial> GetWydzials()
        {
            return db.Wydzialy.ToList();
        }
        public Wydzial GetWydzial(int id)
        {
            return db.Wydzialy.Find(id);
        }
        public List<Stanowisko> GetStanowiskos()
        {
            return db.Stanowiska.ToList();

        }
        public Stanowisko GetStanowisko(int id)
        {
            return db.Stanowiska.Find(id);

        }
        public List<Narzedzie> GetNarzedzias()
        {
            return db.Narzedzia.ToList();

        }
        public Narzedzie GetNarzedzia(int id)
        {
            return db.Narzedzia.Find(id);

        }
    }

}
