import { BaseDto } from "./common/baseDto";

export interface GroupDetailDto extends BaseDto {
    name: string;
    teacherId: number;
    schoolYearId: number;
    schoolYearName: string;
    teacherName: string;
}