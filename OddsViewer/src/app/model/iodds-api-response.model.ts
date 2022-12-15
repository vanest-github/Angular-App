import { MatchedOddsDto } from "./matched-odds-dto.model";

export interface IOddsApiResponse {
    message: string;
    data: MatchedOddsDto[];
}