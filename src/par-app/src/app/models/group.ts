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