export interface ISearchStringService {
    search(url: string, searchString: string): Promise<any>;
}

export interface ITemplatesProvider {
    get(name: string): Promise<(vm: any) => string>;
}
