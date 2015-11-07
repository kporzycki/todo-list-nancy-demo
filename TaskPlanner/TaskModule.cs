using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nancy;
using Nancy.ModelBinding;

namespace TaskPlanner
{
    public class TaskModule : NancyModule
    {
        private static readonly List<PlannedTask> Notes = new List<PlannedTask>();
        private static int _noteCounter;

        static TaskModule()
        {
            AddNote(new PlannedTask() {Description = "Read a book", IsDone = false});
            AddNote(new PlannedTask() {Description = "Make the bed", IsDone = true});
            AddNote(new PlannedTask() {Description = "Eat breakfast", IsDone = false});
            AddNote(new PlannedTask() {Description = "Go out", IsDone = false});
        }

        private static void AddNote(PlannedTask note)
        {
            note.Id = Interlocked.Increment(ref _noteCounter);
            Notes.Add(note);
        }

        public TaskModule()
        {
            Get["/"] = _ => View["manageNotes"];
            Get["/notes/manage"] = _ => View["manageNotes"];

            Get["/notes/"] = _ => Response.AsJson(Notes);

            Post["/notes/"] = _ =>
            {
                var note = this.Bind<PlannedTask>();
                AddNote(note);
                return HttpStatusCode.Created;
            };

            Put["/notes/"] = _ =>
            {
                var note = this.Bind<PlannedTask>();
                var existingNote = Notes.SingleOrDefault(n => n.Id == note.Id);

                if (existingNote == null)
                    return HttpStatusCode.NotFound;

                existingNote.IsDone = note.IsDone;
                existingNote.Description = note.Description;
                return HttpStatusCode.OK;
            };

            Get["/notes/{id:int}"] = p => Response.AsJson(Notes[(int) p.id]);

            Delete["/notes/{id:int}"] = p =>
            {
                var noteToDelete = Notes.SingleOrDefault(n => n.Id == p.id);
                if (noteToDelete != null)
                    Notes.Remove(noteToDelete);
                return HttpStatusCode.OK;
            };
        }
    }

 }
