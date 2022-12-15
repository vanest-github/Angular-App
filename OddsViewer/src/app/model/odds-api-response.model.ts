import { IOddsApiResponse } from "./iodds-api-response.model";
import { MatchedOddsDto } from "./matched-odds-dto.model";

export class OddsApiResponse implements IOddsApiResponse {
    public message: string;
    public data: MatchedOddsDto[];

    constructor() { }
}