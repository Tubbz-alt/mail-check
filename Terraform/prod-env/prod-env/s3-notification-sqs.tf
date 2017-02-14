
resource "aws_s3_bucket_notification" "aggregate_bucket_notification" {
    bucket = "${var.aggregate-report-bucket}"
    queue {
        queue_arn = "${aws_sqs_queue.aggregate-report-queue1.arn}"
        events = ["s3:ObjectCreated:*"]
    }
}
resource "aws_s3_bucket_notification" "forensic_bucket_notification" {
    bucket = "${var.forensic-report-bucket}"
    queue {
        queue_arn = "${aws_sqs_queue.forensic-report-queue1.arn}"
        events = ["s3:ObjectCreated:*"]
    }
}

