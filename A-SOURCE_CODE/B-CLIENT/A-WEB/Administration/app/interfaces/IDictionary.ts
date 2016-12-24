export interface IDictionary<T> {

    // Insert a key into dictionary.
    insert(key: string, value: T);

    // Check whether key has already been in the dictionary.
    containsKey(key: string): boolean;

    // Count number of key-value pairs in dictionary.
    getCount(): number;

    // Find item by using key.
    item(key: string): T;

    // Find list of keys in dictionary.
    keys(): string[];

    // Remove an item from dictionary by using key.
    remove(key: string): T;

    // Find dictionary values list.
    values(): T[];
}