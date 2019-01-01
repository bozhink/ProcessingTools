import { IRequesterBase } from "../contracts/http/requester-base";
import { ISearchStringService } from "../contracts/services";
import { ISearchString } from "../contracts/models/services.models";

export class SearchStringService implements ISearchStringService {

    private requester: IRequesterBase<any>;

    public constructor(requester: IRequesterBase<any>) {
        if (requester == null) {
            throw `Requester is null`;
        }

        this.requester = requester;
    }

    public search(url: string, searchString: string): Promise<any> {
        if (!url) {
            throw `URL is null`;
        }

        if (!searchString) {
            throw `SearchString is null`;
        }

        searchString = searchString.trim();
        if (searchString.length < 1) {
            throw `Search string is empty`;
        }

        let data: ISearchString = {
            searchString: searchString
        };

        return this.requester.post(url, {
            data: data
        });
    }
}