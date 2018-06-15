export interface IDocumentContentData {
    get(url: string): Promise<any>;
    save(url: string, content: string): Promise<any>;
}