import {IDictionary} from "../interfaces/IDictionary";
export class Dictionary<T> implements IDictionary<T> {
    private items: { [index: string]: T } = {};

    // Number of items in the dictionary.
    private count: number = 0;

    // Check whether dictionary contains a specific key or not.
    public containsKey(key: string): boolean {
        return this.items.hasOwnProperty(key);
    }

    // Count the number of key-value pairs in dictionary
    public getCount(): number {
        return this.count;
    }

    // Insert a key-value pair into dictionary.
    public insert(key: string, value: T) {
        this.items[key] = value;
        this.count++;
    }

    // Remove a key-value pair by searching for specific key.
    public remove(key: string): T {
        var val = this.items[key];
        delete this.items[key];
        this.count--;
        return val;
    }

    // Find item by searching key.
    public item(key: string): T {
        return this.items[key];
    }

    // Find list of keys in dictionary.
    public keys(): string[] {
        var keySet: string[] = [];

        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                keySet.push(prop);
            }
        }

        return keySet;
    }

    // Find list of values in dictionary.
    public values(): T[] {
        var values: T[] = [];

        for (var prop in this.items) {
            if (this.items.hasOwnProperty(prop)) {
                values.push(this.items[prop]);
            }
        }

        return values;
    }
}