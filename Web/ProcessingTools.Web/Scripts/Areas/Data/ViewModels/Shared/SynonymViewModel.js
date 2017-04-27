function SynonymViewModel(synonyms) {
    var self = this,
        count = 0;

    self.isModified = ko.observable(false);
    self.count = ko.observable(count);
    self.synonyms = ko.observableArray([]);

    function rebind(synonyms) {
        var i, len, synonym;

        self.synonyms.removeAll();
        count = 0;
        self.count(count);

        if (Array.isArray(synonyms)) {
            len = synonyms.length;
            for (i = 0; i < len; i += 1) {
                synonym = synonyms[i];
                self.synonyms.push(new Synonym(synonym.Id, synonym.name, synonym.languageId, 0));
                count += 1;
            }

            self.count(count);
        }

        self.isModified(false);
    }

    rebind(synonyms);

    self.addSynonym = function () {
        self.synonyms.push(new Synonym(-1, '', '', 2));
        count = self.count() + 1;
        self.count(count);
        self.isModified(true);
    };

    self.removeSynonym = function (synonym) {
        if (synonym) {
            self.synonyms.remove(synonym);
            count = self.count() - 1;
            self.count(count);
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
                    LanguageId: synonym.languageId(),
                    Status: synonym.status() === 0 ? isModified ? 1 : 0 : synonym.status()
                });

                self.isModified(isModified || self.isModified());
            } catch (e) {
                console.log(e);
            }
        }

        return JSON.stringify(data);
    }, self);

    self.save = function (url) {
        return {
            invoke: function () {
                $.ajax({
                    url: url,
                    method: 'POST',
                    contentType: 'application/json',
                    data: self.json(),
                    success: function (data) {
                        rebind(data);
                    },
                    error: function (error) {
                        alert(JSON.stringify(error));
                    }
                });
            }
        };
    };
}