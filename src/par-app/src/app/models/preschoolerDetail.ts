import { BaseDto } from "./common/baseDto";

export interface PreschoolerDetailDto extends BaseDto {
    firstName: string;
    lastName: string;
    groupId: number;
    groupName: string;
}