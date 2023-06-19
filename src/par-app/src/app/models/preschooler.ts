import {BaseDto} from "./common/baseDto";
import {GroupDto} from "./group";

export interface PreschoolerDto extends BaseDto {
    firstName: string;
    lastName: string;
    groupId: number;
    group: GroupDto | null;
}