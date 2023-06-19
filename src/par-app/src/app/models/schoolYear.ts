import {BaseDto} from "./common/baseDto";
import {GroupDto} from "./group";

export interface SchoolYearDto extends BaseDto {
    startDate: Date;
    endDate: Date;
    isCurrent: boolean;
    groups: GroupDto[];
}

export class SchoolYearFormValues {
    id: number = 0;
    startDate: Date | null = null;
    endDate: Date  | null = null;
    isCurrent: boolean = false;

    constructor(schoolYear?: SchoolYearDto) {
        if (schoolYear) {
            this.id = schoolYear.id;
            this.startDate = schoolYear.startDate;
            this.endDate = schoolYear.endDate;
            this.isCurrent = schoolYear.isCurrent;
        }
    }
}