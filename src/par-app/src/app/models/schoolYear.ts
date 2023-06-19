import {BaseDto} from "./common/baseDto";
import {GroupDto} from "./group";

export interface SchoolYearDto extends BaseDto {
    startDate: Date;
    endDate: Date;
    isCurrent: boolean;
    groups: GroupDto[];
}