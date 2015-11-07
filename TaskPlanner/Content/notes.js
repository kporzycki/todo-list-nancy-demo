function AppViewModel() {
    "use strict";

    var notes = ko.observableArray(),
        description = ko.observable();

    function reloadNotes() {
        $.getJSON("/notes", function (data) {
            notes(data);
        });
    }
    
    this.notes = notes;
    this.description = description;

    this.deleteNote = function(note) {
        $.ajax({
            url: '/notes/' + note.id,
            type: 'DELETE',
            success: reloadNotes
        });
    }

    this.addNote = function addNote() {
        var note = {
            isDone: false,
            description: description()
        }
        description("");
        $.post("/notes", note, reloadNotes);
    }

    this.putNote = function putNote(note) {
        $.ajax({
            url: '/notes/',
            type: 'PUT',
            data: note,
            success: reloadNotes
        });
    }

    this.reloadNotes = reloadNotes();

    reloadNotes();
}

ko.applyBindings(new AppViewModel());