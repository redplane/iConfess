"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var KeyValuePair_1 = require("../models/KeyValuePair");
// Dictionary class.
var Dictionary = (function () {
    //#endregion
    //#region Constructor
    // Initiate dictionary with default settings.
    function Dictionary() {
        // Initiate key-value pairs list.
        this.keyValuePairs = new Array();
    }
    //#endregion
    //#region Methods
    // Insert a new key-value pair into dictionary.
    Dictionary.prototype.add = function (key, value) {
        // Key exists.
        if (this.containsKey(key)) {
            throw Error(key + " exists in dictionary");
        }
        var keyValuePair = new KeyValuePair_1.KeyValuePair();
        keyValuePair.key = key;
        keyValuePair.value = value;
        this.keyValuePairs.push(keyValuePair);
    };
    // Remove a record by using specific key.
    Dictionary.prototype.remove = function (key) {
        var _this = this;
        // Find the key in key-value pairs.
        var results = this.keyValuePairs
            .filter(function (x) {
            return x.key == key;
        })
            .map(function (x) {
            return _this.keyValuePairs.indexOf(x);
        });
        for (var index = 0; index < results.length; index++)
            this.keyValuePairs.splice(index, 1);
    };
    // Get all keys in dictionary.
    Dictionary.prototype.keys = function () {
        return this.keyValuePairs.map(function (x) {
            return x.key;
        });
    };
    // Get all values in dictionary.
    Dictionary.prototype.values = function () {
        return this.keyValuePairs.map(function (x) {
            return x.value;
        });
    };
    // Check whether the designated key has been registered or not.
    Dictionary.prototype.containsKey = function (key) {
        // Find item using key.
        var item = this.getKeyItem(key);
        if (item == null)
            return false;
        return true;
    };
    // Find key item.
    Dictionary.prototype.getKeyItem = function (key) {
        // Array doesn't contain key value pair before.
        if (this.keyValuePairs == null || this.keyValuePairs.length < 1)
            return null;
        // Filter key-value pairs using key name.
        var results = this.keyValuePairs.filter(function (x) {
            return x.key == key;
        });
        // Key doesn't exist.
        if (results == null || results.length < 1)
            return null;
        return results[0].value;
    };
    // Get list of key-value pairs in dictionary.
    Dictionary.prototype.getKeyValuePairs = function () {
        return this.keyValuePairs;
    };
    return Dictionary;
}());
exports.Dictionary = Dictionary;
//# sourceMappingURL=Dictionary.js.map