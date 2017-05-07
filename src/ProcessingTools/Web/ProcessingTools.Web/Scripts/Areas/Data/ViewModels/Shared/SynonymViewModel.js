function SynonymViewModel(synonyms) {
    var self = this;

    self.isModified = ko.observable(false);
    self.count = ko.observable(0);
    self.synonyms = ko.observableArray([]);

    function rebind(synonyms) {
        var i, len, synonym, count = 0;
        self.synonyms.removeAll();
        self.count(count);

        if (Array.isArray(synonyms)) {
            len = synonyms.length;
            for (i = 0; i < len; i += 1) {
                synonym = synonyms[i];
                self.synonyms.push(new Synonym(synonym.Id, synonym.Name, synonym.LanguageCode, 0));
                count += 1;
            }

            self.count(count);
        }

        self.isModified(false);
    }

    rebind(synonyms);

    self.addSynonym = function () {
        self.synonyms.push(new Synonym(-1, '', '', 2));
        self.count(self.count() + 1);
        self.isModified(true);
    };

    self.removeSynonym = function (synonym) {
        if (synonym) {
            self.synonyms.remove(synonym);
            self.count(self.count() - 1);
            if (synonym.status() < 2) {
                self.synonyms.push(new Synonym(synonym.id, synonym.name(), synonym.languageId(), 3));
                self.isModified(true);
            }
        }
    };

    self.json = ko.computed(function () {
        var i, len, isModified, synonym, data = [];

        len = self.synonyms().length;
        for (i = 0; i < len; i += 1) {
            try {
                synonym = self.synonyms()[i];
                isModified = synonym.name.isModified() || synonym.languageId.isModified();
                data.push({
                    Id: synonym.id,
                    Name: synonym.name(),
                    LanguageCode: synonym.languageId(),
                    Status: synonym.status() === 0 ? isModified ? 1 : 0 : synonym.status()
                });

                self.isModified(isModified || self.isModified());
            } catch (e) {
                console.log(e);
            }
        }

        return JSON.stringify(data);
    }, self);
}