import {BaseDto} from "./common/baseDto";
import {GroupDto} from "./group";

export interface PreschoolerDto extends BaseDto {
    firstName: string;
    lastName: string;
    groupId: number;
    group: GroupDto | null;
}

export class PreschoolerFormValues {
    id: number = 0;
    firstName: string = '';
    lastName: string = '';
    groupId: number = 0;

    constructor(preschoolerDto?: PreschoolerDto) {
        if (preschoolerDto) {
            this.id = preschoolerDto.id;
            this.firstName = preschoolerDto.firstName;
            this.lastName = preschoolerDto.lastName;
            this.groupId = preschoolerDto.groupId;
        }
    }
}