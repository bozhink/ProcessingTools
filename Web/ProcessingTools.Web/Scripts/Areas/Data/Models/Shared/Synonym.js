function Synonym(id, name, languageId, status) {
    var self = this;

    self.id = id;
    self.name = ko.observable(name).extend({ required: true, minLength: 3 });
    self.languageId = ko.observable(languageId).extend({ required: true });
    self.status = ko.observable(status);
}
