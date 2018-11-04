export interface IDocumentContentData {
    get(url: string): Promise<any>;
    initializeContent(content: string): void;
    save(url: string, content: string): Promise<any>;
}