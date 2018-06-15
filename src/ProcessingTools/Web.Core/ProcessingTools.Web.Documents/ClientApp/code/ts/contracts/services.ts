export interface ISearchStringService {
    search(url: string, searchString: string): Promise<any>;
}