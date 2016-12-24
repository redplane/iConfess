"use strict";
var Dictionary = (function () {
    function Dictionary() {
        this.items = {};
        // Number of items in the dictionary.
        this.count = 0;
    }
    // Check whether dictionary contains a specific key or not.
    Dictionary.prototype.containsKey = function (key) {
        return this.items.hasOwnProperty(key);
    };
    // Count the number of key-value pairs in dictionary
    Dictionary.prototype.getCount = function () {
        return this.count;
    };
    // Insert a key-value pair into dictionary.
    Dictionary.prototype.insert = function (key, value) {
        this.items[key] = value;
        this.count++;
    };
    // Remove a key-value pair by searching for specific key.
    Dictionary.prototype.remove = function (key) {
        var val = this.items[key];
        delete this.items[key];
        this.count--;
        return val;
    };
    // Find item by searching key.
    Dictionary.prototype.item = function (key) {
        return this.items[key];
    };
    // Find list of keys in dictionary.
    Dictionary.prototype.keys = function () {
        var keySet = [];
        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                keySet.push(prop);
            }
        }
        return keySet;
    };
    // Find list of values in dictionary.
    Dictionary.prototype.values = function () {
        var values = [];
        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                values.push(this.items[prop]);
            }
        }
        return values;
    };
    return Dictionary;
}());
exports.Dictionary = Dictionary;
//# sourceMappingURL=Dictionary.js.map