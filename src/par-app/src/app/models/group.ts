import { BaseDto } from "./common/baseDto";
import {SchoolYearDto} from "./schoolYear";
import {ApplicationUser} from "./applicationUser";
import {PreschoolerDto} from "./preschooler";

export interface GroupDto extends BaseDto {
    name: string;
    teacherId: number;
    schoolYearId: number;
    schoolYear: SchoolYearDto | null;
    teacher: ApplicationUser | null;
    preschoolers: PreschoolerDto[];
}

export class GroupFormValues {
    id: number = 0;
    name: string = '';
    teacherId: number = 0;
    schoolYearId: number = 0;

    constructor(schoolYear?: GroupDto) {
        if (schoolYear) {
            this.id = schoolYear.id;
            this.name = schoolYear.name;
            this.teacherId = schoolYear.teacherId;
            this.schoolYearId = schoolYear.schoolYearId;
        }
    }
}