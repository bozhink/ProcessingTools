/**
 *  Adapted from http://www.rcn.montana.edu/Scripts/utmconv.js
 */
/*
 *
 * A UTM -> Lat/Long (or vice versa) converter adapted from the script used at
 *     http://www.uwgb.edu/dutchs/UsefulData/ConvertUTMNoOZ.HTM
 * I've taken the calculations portion of his script and turned it into a singleton
 * javascript object so that it is no longer dependent on the controls used on the page
 * to use this script, call setDatum with the index of the datum to be used and then
 * call the various conversion functions latLngToUtm(), utmToLatLng() or natoToLatLng(), utmToNato(), natoToUtm()
 * to convert between the various coordinate systems, hopefully accurately!
 *
 * NOTE: no attempt is made to compensate for the irregular grid in the area around the southern coast of Norway and
 * Svalbard (zones 32V and 31X, 33X, 35X and 37X) because of this results returned for NATO coordinates for lat/lng or
 * UTM values located in these regions will not be correct.
 *
 */

export interface IDatum {
    eqRad: number;
    flat: number;
}

export interface INatoUtm {
    northing: number;
    easting: number;
    zone: number;
    southern: boolean;
}

export interface IUtmNato {
    northing: number;
    easting: number;
    latZone: string;
    lngZone: number;
    digraph: string;
}

export interface INatoLatLng {
    lat: number;
    lng: number;
}

export interface IUtmLatLng {
    lat: number;
    lng: number;
}

export interface ILatLngUtm {
    global: INatoUtm;
    nato: IUtmNato;
}

export interface IUtmConverter {
    a: number;
    f: number;
    b: number;
    e: number;
    e0: number;
    k: number;
    k0: number;
    drad: number;
    digraphLettersE: string;
    digraphLettersN: string;
    digraphLettersAll: string;
    datumTable: Array<IDatum>;
    setDatum: (index: number) => void;
    latLngToUtm: (lat: number, lngd: number) => ILatLngUtm;
    utmToLatLng: (x: number, y: number, utmz: number, southern: boolean) => IUtmLatLng;
    natoToLatLng: (utme: number, utmn: number, utmz: number, latz: string, digraph: string) => INatoLatLng;
    natoToUtm: (utme: number, utmn: number, utmz: number, latz: string, digraph: string) => INatoUtm;
    utmToNato: (x: number, y: number, utmz: number, southern: boolean) => IUtmNato;
    makeDigraph: (x: number, y: number, utmz: number) => string;
}

export let utmconv: IUtmConverter = {
    //
    // constants taken from or calculated from the datum
    //
    a: 0,   // equatorial radius in meters
    f: 0,   // polar flattening
    b: 0,   // polar radius in meters
    e: 0,   // eccentricity
    e0: 0,  // e'


    //
    // constants used in calculations
    //
    k: 1,
    k0: 0.9996,
    drad: Math.PI / 180,

    digraphLettersE: "ABCDEFGHJKLMNPQRSTUVWXYZ",
    digraphLettersN: "ABCDEFGHJKLMNPQRSTUV",
    // tslint:disable-next-line:max-line-length
    digraphLettersAll: "ABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUVABCDEFGHJKLMNPQRSTUV",
    datumTable: [
        { eqRad: 6378137.0, flat: 298.2572236 }, // datum WGS 84
        { eqRad: 6378137.0, flat: 298.2572236 }, // datum NAD 83
        { eqRad: 6378137.0, flat: 298.2572215 }, // datum GRS 80
        { eqRad: 6378135.0, flat: 298.2597208 }, // datum WGS 72
        { eqRad: 6378160.0, flat: 298.2497323 }, // datum Austrailian 1965
        { eqRad: 6378245.0, flat: 298.2997381 }, // datum Krasovsky 1940
        { eqRad: 6378206.4, flat: 294.9786982 }, // datum North American 1927
        { eqRad: 6378388.0, flat: 296.9993621 }, // datum International 1924
        { eqRad: 6378388.0, flat: 296.9993621 }, // datum Hayford 1909
        { eqRad: 6378249.1, flat: 293.4660167 }, // datum Clarke 1880
        { eqRad: 6378206.4, flat: 294.9786982 }, // datum Clarke 1866
        { eqRad: 6377563.4, flat: 299.3247788 }, // datum Airy 1830
        { eqRad: 6377397.2, flat: 299.1527052 }, // datum Bessel 1841
        { eqRad: 6377276.3, flat: 300.8021499 }  // datum Everest 1830
    ],

    ///
    /// calculate constants used for doing conversions using a given map datum
    ///
    setDatum: function (index: number): void {
        let datum: IDatum = this.datumTable[index];
        this.a = datum.eqRad;
        this.f = 1 / datum.flat;
        this.b = this.a * (1 - this.f);   // polar radius
        this.e = Math.sqrt(1 - Math.pow(this.b, 2) / Math.pow(this.a, 2));
        this.e0 = this.e / Math.sqrt(1 - Math.pow(this.e, 1));
    },

    ///
    /// given a lat/lng pair, returns both global UTM and NATO UTM in the following form:
    /// utm:
    /// {
    ///     global: { northing: n, easting: e, zone: z, southern: x },
    ///     nato: { northing: n, easting: e, latzone: z0, lngzone: z1, digraph: xx }
    /// }
    ///
    /// this function assumes that all data validation has been performed prior to calling
    /// it.
    ///
    latLngToUtm: function (lat: number, lngd: number): ILatLngUtm {
        let phi: number = lat * this.drad;                              // convert latitude to radians
        let lng: number = lngd * this.drad;                             // convert longitude to radians
        let utmz: number = 1 + Math.floor((lngd + 180) / 6);            // longitude to utm zone
        let zcm: number = 3 + 6 * (utmz - 1) - 180;                     // central meridian of a zone
        let latz: number = 0;                                           // this gives us zone A-B for below 80S
        let esq: number = (1 - (this.b / this.a) * (this.b / this.a));
        let e0sq: number = this.e * this.e / (1 - Math.pow(this.e, 2));
        let M: number = 0;

        // convert latitude to latitude zone for nato
        if (lat > -80 && lat < 72) {
            latz = Math.floor((lat + 80) / 8) + 2;      // zones C-W in this range
        } if (lat > 72 && lat < 84) {
            latz = 21;                                  // zone X
        } else if (lat > 84) {
            latz = 23;                                  // zone Y-Z
        }

        let N: number = this.a / Math.sqrt(1 - Math.pow(this.e * Math.sin(phi), 2));
        let T: number = Math.pow(Math.tan(phi), 2);
        let C: number = e0sq * Math.pow(Math.cos(phi), 2);
        let A: number = (lngd - zcm) * this.drad * Math.cos(phi);

        // calculate M (USGS style)
        M = phi * (1 - esq * (1 / 4 + esq * (3 / 64 + 5 * esq / 256)));
        M = M - Math.sin(2 * phi) * (esq * (3 / 8 + esq * (3 / 32 + 45 * esq / 1024)));
        M = M + Math.sin(4 * phi) * (esq * esq * (15 / 256 + esq * 45 / 1024));
        M = M - Math.sin(6 * phi) * (esq * esq * esq * (35 / 3072));
        M = M * this.a;                                      // arc length along standard meridian

        let M0: number = 0;                                  // if another point of origin is used than the equator

        // now we are ready to calculate the UTM values...
        // first the easting
        // tslint:disable-next-line:max-line-length
        let x: number = this.k0 * N * A * (1 + A * A * ((1 - T + C) / 6 + A * A * (5 - 18 * T + T * T + 72 * C - 58 * e0sq) / 120)); // easting relative to CM
        x = x + 500000; // standard easting

        // now the northing
        // tslint:disable-next-line:max-line-length
        let y: number = this.k0 * (M - M0 + N * Math.tan(phi) * (A * A * (1 / 2 + A * A * ((5 - T + 9 * C + 4 * C * C) / 24 + A * A * (61 - 58 * T + T * T + 600 * C - 330 * e0sq) / 720))));    // first from the equator
        let yg: number = y + 10000000;  // yg = y global, from S. Pole
        if (y < 0) {
            y = 10000000 + y;   // add in false northing if south of the equator
        }

        let digraph: string = this.makeDigraph(x, y, utmz);
        let rv: ILatLngUtm = {
            global: {
                easting: Math.round(10 * (x)) / 10,
                northing: Math.round(10 * y) / 10,
                zone: utmz,
                southern: phi < 0
            },
            nato: {
                easting: Math.round(10 * (x - 100000 * Math.floor(x / 100000))) / 10,
                northing: Math.round(10 * (y - 100000 * Math.floor(y / 100000))) / 10,
                latZone: this.digraphLettersN[latz],
                lngZone: utmz,
                digraph: digraph
            }
        };

        return rv;
    },

    ///
    /// convert a set of global UTM coordinates to lat/lng returned as follows
    ///
    /// { lat: y, lng: x }
    ///
    /// inputs:
    ///     x: easting
    ///     y: northing
    ///     utmz: utm zone
    ///     southern: bool indicating coords are in southern hemisphere
    ///
    utmToLatLng: function (x: number, y: number, utmz: number, southern: boolean): IUtmLatLng {
        let esq: number = (1 - (this.b / this.a) * (this.b / this.a));
        let e0sq: number = this.e * this.e / (1 - Math.pow(this.e, 2));
        let zcm: number = 3 + 6 * (utmz - 1) - 180; // central meridian of zone
        let e1: number = (1 - Math.sqrt(1 - Math.pow(this.e, 2))) / (1 + Math.sqrt(1 - Math.pow(this.e, 2)));
        let M0: number = 0;
        let M: number = 0;

        if (!southern) {
            M = M0 + y / this.k0; // arc length along standard meridian.
        } else {
            M = M0 + (y - 10000000) / this.k;
        }

        let mu: number = M / (this.a * (1 - esq * (1 / 4 + esq * (3 / 64 + 5 * esq / 256))));
        // tslint:disable-next-line:max-line-length
        let phi1: number = mu + e1 * (3 / 2 - 27 * e1 * e1 / 32) * Math.sin(2 * mu) + e1 * e1 * (21 / 16 - 55 * e1 * e1 / 32) * Math.sin(4 * mu); // footprint Latitude
        phi1 = phi1 + e1 * e1 * e1 * (Math.sin(6 * mu) * 151 / 96 + e1 * Math.sin(8 * mu) * 1097 / 512);
        let C1: number = e0sq * Math.pow(Math.cos(phi1), 2);
        let T1: number = Math.pow(Math.tan(phi1), 2);
        let N1: number = this.a / Math.sqrt(1 - Math.pow(this.e * Math.sin(phi1), 2));
        let R1: number = N1 * (1 - Math.pow(this.e, 2)) / (1 - Math.pow(this.e * Math.sin(phi1), 2));
        let D: number = (x - 500000) / (N1 * this.k0);
        let phi: number = (D * D) * (1 / 2 - D * D * (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * e0sq) / 24);
        phi = phi + Math.pow(D, 6) * (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * e0sq - 3 * C1 * C1) / 720;
        phi = phi1 - (N1 * Math.tan(phi1) / R1) * phi;

        let lat: number = Math.floor(1000000 * phi / this.drad) / 1000000;
        // tslint:disable-next-line:max-line-length
        let lng: number = D * (1 + D * D * ((-1 - 2 * T1 - C1) / 6 + D * D * (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * e0sq + 24 * T1 * T1) / 120)) / Math.cos(phi1);
        lng = zcm + lng / this.drad;

        return {
            lat: lat,
            lng: lng
        };
    },

    ///
    /// takes a set of NATO style UTM coordinates and converts them to a lat/lng pair.
    ///
    /// { lat: y, lng: x }
    ///
    /// inputs:
    ///     utme: easting
    ///     utmn: northing
    ///     utmz: longitudinal zone
    ///     latz: character representing latitudinal zone
    ///     digraph: string representing grid
    ///
    ///
    natoToLatLng: function (utme: number, utmn: number, utmz: number, latz: string, digraph: string): INatoLatLng {
        let coords: INatoUtm = this.natoToUtm(utme, utmn, utmz, latz, digraph);
        return this.utmToLatLng(coords.easting, coords.northing, coords.zone, coords.southern);
    },

    ///
    /// convert a set of nato coordinates to the global system.  returns a structure
    ///
    /// { norhting: y, easting: x, zone: zone, southern: hemisphere }
    ///
    /// inputs:
    ///     utme: easting
    ///     utmn: northing
    ///     utmz: longitudinal zone
    ///     latz: character representing latitudinal zone
    ///     digraph: string representing grid
    ///
    /// checks for digraph validity
    ///
    natoToUtm: function (utme: number, utmn: number, utmz: number, latz: string, digraph: string): INatoUtm {
        latz = latz.toUpperCase();
        digraph = digraph.toUpperCase();

        let eltr: string = digraph.charAt(0);
        let nltr: string = digraph.charAt(1);

        ///
        /// make sure the digraph is consistent
        ///
        if (nltr === "I" || eltr === "O") {
            throw "I and O are not legal digraph characters";
        }

        if (nltr === "W" || nltr === "X" || nltr === "Y" || nltr === "Z") {
            throw "W, X, Y and Z are not legal second characters in a digraph";
        }

        let eidx: number = this.digraphLettersE.indexOf(eltr);
        let nidx: number = this.digraphLettersN.indexOf(nltr);

        if (utmz / 2 === Math.floor(utmz / 2)) {
            nidx -= 5; // correction for even numbered zones
        }

        let ebase: number = 100000 * (1 + eidx - 8 * Math.floor(eidx / 8));

        let latBand: number = this.digraphLettersE.indexOf(latz);
        let latBandLow: number = 8 * latBand - 96;
        let latBandHigh: number = 8 * latBand - 88;

        if (latBand < 2) {
            latBandLow = -90;
            latBandHigh = -80;
        } else if (latBand === 21) {
            latBandLow = 72;
            latBandHigh = 84;
        } else if (latBand > 21) {
            latBandLow = 84;
            latBandHigh = 90;
        }

        let lowLetter: number = Math.floor(100 + 1.11 * latBandLow);
        let highLetter: number = Math.round(100 + 1.11 * latBandHigh);
        let latBandLetters: string = null;
        if (utmz / 2 === Math.floor(utmz / 2)) {
            latBandLetters = this.digraphLettersAll.slice(lowLetter + 5, highLetter + 5);
        } else {
            latBandLetters = this.digraphLettersAll.slice(lowLetter, highLetter);
        }
        let nbase: number = 100000 * (lowLetter + latBandLetters.indexOf(nltr));

        let x: number = ebase + utme;
        let y: number = nbase + utmn;
        if (y > 10000000) {
            y = y - 10000000;
        }

        if (nbase >= 10000000) {
            y = nbase + utmn - 10000000;
        }

        let southern: boolean = nbase < 10000000;

        return { northing: y, easting: x, zone: utmz, southern: southern };
    },

    ///
    /// returns a set of nato coordinates from a set of global UTM coordinates
    /// return is an object with the following structure:
    ///     { northing: n, easting: e, latZone: z0, lngZone: z1, digraph: xx }
    ///
    /// inputs:
    ///     x: easting
    ///     y: northing
    ///     utmz: the utm zone
    ///     southern: hemisphere indicator
    ///
    utmToNato: function (x: number, y: number, utmz: number, southern: boolean): IUtmNato {
        let esq: number = (1 - (this.b / this.a) * (this.b / this.a));
        let e0sq: number = this.e * this.e / (1 - Math.pow(this.e, 2));
        let e1: number = (1 - Math.sqrt(1 - Math.pow(this.e, 2))) / (1 + Math.sqrt(1 - Math.pow(this.e, 2)));
        let M0: number = 0;
        let M: number = 0;

        if (!southern) {
            M = M0 + y / this.k0; // arc length along standard meridian.
        } else {
            M = M0 + (y - 10000000) / this.k;
        }

        //
        // calculate the latitude so that we can derive the latitude zone
        //
        let mu: number = M / (this.a * (1 - esq * (1 / 4 + esq * (3 / 64 + 5 * esq / 256))));
        // tslint:disable-next-line:max-line-length
        let phi1: number = mu + e1 * (3 / 2 - 27 * e1 * e1 / 32) * Math.sin(2 * mu) + e1 * e1 * (21 / 16 - 55 * e1 * e1 / 32) * Math.sin(4 * mu);   // footprint Latitude
        phi1 = phi1 + e1 * e1 * e1 * (Math.sin(6 * mu) * 151 / 96 + e1 * Math.sin(8 * mu) * 1097 / 512);
        let C1: number = e0sq * Math.pow(Math.cos(phi1), 2);
        let T1: number = Math.pow(Math.tan(phi1), 2);
        let N1: number = this.a / Math.sqrt(1 - Math.pow(this.e * Math.sin(phi1), 2));
        let R1: number = N1 * (1 - Math.pow(this.e, 2)) / (1 - Math.pow(this.e * Math.sin(phi1), 2));
        let D: number = (x - 500000) / (N1 * this.k0);
        let phi: number = (D * D) * (1 / 2 - D * D * (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * e0sq) / 24);
        phi = phi + Math.pow(D, 6) * (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * e0sq - 3 * C1 * C1) / 720;
        phi = phi1 - (N1 * Math.tan(phi1) / R1) * phi;
        let lat: number = Math.floor(1000000 * phi / this.drad) / 1000000;
        let latz: number;

        // convert latitude to latitude zone for nato
        if (lat > -80 && lat < 72) {
            latz = Math.floor((lat + 80) / 8) + 2;      // zones C-W in this range
        } if (lat > 72 && lat < 84) {
            latz = 21;                                  // zone X
        } else if (lat > 84) {
            latz = 23;                                  // zone Y-Z
        }

        let digraph: string = this.makeDigraph(x, y, utmz);
        x = Math.round(10 * (x - 100000 * Math.floor(x / 100000))) / 10;
        y = Math.round(10 * (y - 100000 * Math.floor(y / 100000))) / 10;

        return {
            easting: x,
            northing: y,
            latZone: this.digraphLettersN[latz],
            lngZone: utmz,
            digraph: digraph
        };
    },

    ///
    /// create a nato grid digraph.
    ///
    /// inputs:
    ///     x: easting
    ///     y: northing
    ///     utmz: utm zone
    ///
    makeDigraph: function (x: number, y: number, utmz: number): string {
        //
        // first get the east digraph letter
        //
        let letter: number = Math.floor((utmz - 1) * 8 + (x) / 100000);
        letter = letter - 24 * Math.floor(letter / 24) - 1;
        let digraph: string = this.digraphLettersE.charAt(letter);

        letter = Math.floor(y / 100000);

        if (utmz / 2 === Math.floor(utmz / 2)) {
            letter = letter + 5;
        }

        letter = letter - 20 * Math.floor(letter / 20);
        digraph = digraph + this.digraphLettersN.charAt(letter);

        return digraph;
    }
};
