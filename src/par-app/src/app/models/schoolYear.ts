import {BaseDto} from "./common/baseDto";
import {GroupDto} from "./group";

export interface SchoolYearDto extends BaseDto {
    name: string;
    startDate: Date;
    endDate: Date;
    isCurrent: boolean;
    groups: GroupDto[];
}

export class SchoolYearFormValues {
    id: number = 0;
    name: string = '';
    startDate: Date = new Date();
    endDate: Date = new Date();
    isCurrent: boolean = false;

    constructor(schoolYear?: SchoolYearDto) {
        if (schoolYear) {
            this.id = schoolYear.id;
            this.name = schoolYear.name;
            this.startDate = schoolYear.startDate;
            this.endDate = schoolYear.endDate;
            this.isCurrent = schoolYear.isCurrent;
        }
    }
}